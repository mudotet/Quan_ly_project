using Quan_ly_project.Data;
using Quan_ly_project.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace Quan_ly_project.ViewModels
{
    public class ProjectMemberViewModel : BaseViewModel
    {
        private readonly ProjectMemberRepository _projectMemberRepository;
        private ObservableCollection<ProjectMember> _members;

        public ObservableCollection<ProjectMember> Members
        {
            get => _members;
            set => SetProperty(ref _members, value);
        }

        public ICommand AddMemberCommand { get; }
        public ICommand GoBackCommand { get; }
        public ICommand RemoveMemberCommand { get; }
        public ICommand EditMemberCommand { get; }

        public ProjectMemberViewModel()
        {
            _projectMemberRepository = new ProjectMemberRepository();
            Members = new ObservableCollection<ProjectMember>();
            GoBackCommand = new Command(OnGoBack);
            AddMemberCommand = new Command(OnAddMember);
            RemoveMemberCommand = new Command<int>(OnRemoveMember);
            EditMemberCommand = new Command<ProjectMember>(OnEditMember);

            LoadProjectMembers(1); // Assuming we are loading members for project with ID 1 (change as needed)
        }

        // Load danh sách thành viên của dự án
        private void LoadProjectMembers(int projectId)
        {
            var members = _projectMemberRepository.GetMembersByProjectId(projectId);
            Members.Clear();
            foreach (var member in members)
            {
                Members.Add(member);
            }
        }

        // Thêm thành viên vào dự án
        private async void OnAddMember()
        {
            // Mở cửa sổ nhập thông tin thành viên
            var userName = await Application.Current.MainPage.DisplayPromptAsync("Thêm Thành Viên", "Nhập tên người dùng:", "OK", "Hủy", null, -1, Keyboard.Default, "");
            if (string.IsNullOrWhiteSpace(userName)) return;

            var role = await Application.Current.MainPage.DisplayPromptAsync("Thêm Thành Viên", "Nhập vai trò (e.g. Developer, Manager):", "OK", "Hủy", null, -1, Keyboard.Default, "");
            if (string.IsNullOrWhiteSpace(role)) return;

            // Giả sử người dùng nhập UserId là 2 và ProjectId là 1
            var newMember = new ProjectMember
            {
                ProjectId = 1,    // ID của dự án
                UserId = 2,       // ID người dùng
                UserName = userName,
                Role = role,
                JoinedAt = DateTime.Now
            };

            _projectMemberRepository.AddProjectMember(newMember); // Thêm thành viên vào cơ sở dữ liệu
            LoadProjectMembers(1); // Tải lại danh sách thành viên sau khi thêm
        }

        // Cập nhật thông tin thành viên
        private async void OnEditMember(ProjectMember member)
        {
            if (member == null) return;

            var role = await Application.Current.MainPage.DisplayPromptAsync("Chỉnh Sửa Thành Viên", "Nhập vai trò mới:", "OK", "Hủy", member.Role);
            if (role != null) member.Role = role;

            _projectMemberRepository.UpdateProjectMember(member);
            LoadProjectMembers(1); // Tải lại danh sách thành viên sau khi cập nhật
        }

        // Xóa thành viên khỏi dự án
        private async void OnRemoveMember(int userId)
        {
            bool confirm = await Application.Current.MainPage.DisplayAlert("Xác nhận", "Bạn có chắc chắn muốn xóa thành viên này?", "Có", "Không");
            if (confirm)
            {
                _projectMemberRepository.RemoveMember(1, userId); // ProjectId là 1, thay thế với logic thực tế
                LoadProjectMembers(1); // Tải lại danh sách sau khi xóa
            }
        }

        // Điều hướng quay lại
        private async void OnGoBack()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}