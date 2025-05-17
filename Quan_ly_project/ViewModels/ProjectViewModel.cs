using Quan_ly_project.Data;
using Quan_ly_project.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace Quan_ly_project.ViewModels
{
    public class ProjectViewModel : BaseViewModel
    {
        public ObservableCollection<Project> Projects { get; }
        private readonly ProjectRepository _projectRepository;

        public ICommand AddProjectCommand { get; }
        public ICommand EditProjectCommand { get; }
        public ICommand DeleteProjectCommand { get; }
        public ICommand GoBackCommand { get; }

        public ProjectViewModel()
        {
            Projects = new ObservableCollection<Project>();
            _projectRepository = new ProjectRepository();

            GoBackCommand = new Command(OnGoBack);
            AddProjectCommand = new Command(OnAddProject);
            EditProjectCommand = new Command<Project>(OnEditProject); // Đảm bảo nhận Project tham số
            DeleteProjectCommand = new Command<int>(OnDeleteProject); // Chuyển thành Command<int> thay vì Project

            LoadProjects(); // Load danh sách dự án khi view được khởi tạo
        }

        // Load danh sách các dự án
        private void LoadProjects()
        {
            var projects = _projectRepository.GetAllProjects(); // Lấy danh sách dự án từ repository
            Projects.Clear();
            foreach (var project in projects)
            {
                Projects.Add(project);
            }
        }

        // Thêm dự án mới
        private async void OnAddProject()
        {
            var name = await Application.Current.MainPage.DisplayPromptAsync("Thêm Dự Án", "Nhập tên dự án:", "OK", "Hủy", null, -1, Keyboard.Default, "");
            if (string.IsNullOrWhiteSpace(name)) return;

            var description = await Application.Current.MainPage.DisplayPromptAsync("Thêm Dự Án", "Nhập mô tả dự án:", "OK", "Hủy", null, -1, Keyboard.Default, "");
            if (description == null) description = "";

            var startDateString = await Application.Current.MainPage.DisplayPromptAsync("Thêm Dự Án", "Nhập ngày bắt đầu (dd/MM/yyyy):", "OK", "Hủy", null, -1, Keyboard.Default, DateTime.Now.ToString("dd/MM/yyyy"));
            if (!DateTime.TryParseExact(startDateString, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime startDate))
            {
                await Application.Current.MainPage.DisplayAlert("Lỗi", "Định dạng ngày không hợp lệ.", "OK");
                return;
            }

            var endDateString = await Application.Current.MainPage.DisplayPromptAsync("Thêm Dự Án", "Nhập ngày kết thúc (dd/MM/yyyy):", "OK", "Hủy", null, -1, Keyboard.Default, DateTime.Now.AddMonths(1).ToString("dd/MM/yyyy"));
            if (!DateTime.TryParseExact(endDateString, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime endDate))
            {
                await Application.Current.MainPage.DisplayAlert("Lỗi", "Định dạng ngày không hợp lệ.", "OK");
                return;
            }

            var newProject = new Project
            {
                Name = name,
                Description = description,
                StartDate = startDate,
                EndDate = endDate,
                CreatedBy = 1, // Giả sử là người dùng ID = 1
                CreatedAt = DateTime.Now
            };

            _projectRepository.AddProject(newProject);
            LoadProjects(); // Tải lại danh sách dự án sau khi thêm
        }

        private async void OnEditProject(Project project)
        {
            if (project == null) return;

            // Hiển thị thông tin hiện tại của dự án cho người dùng chỉnh sửa
            var name = await Application.Current.MainPage.DisplayPromptAsync(
                "Chỉnh Sửa Dự Án",
                "Nhập tên dự án:",
                "OK", "Hủy",
                project.Name); // Hiển thị tên hiện tại

            if (string.IsNullOrWhiteSpace(name)) return;
            project.Name = name; // Cập nhật tên dự án

            var description = await Application.Current.MainPage.DisplayPromptAsync(
                "Chỉnh Sửa Dự Án",
                "Nhập mô tả dự án:",
                "OK", "Hủy",
                project.Description); // Hiển thị mô tả hiện tại

            if (description != null) project.Description = description; // Cập nhật mô tả

            var startDateString = await Application.Current.MainPage.DisplayPromptAsync(
                "Chỉnh Sửa Dự Án",
                "Nhập ngày bắt đầu (dd/MM/yyyy):",
                "OK", "Hủy",
                project.StartDate.ToString("dd/MM/yyyy")); // Hiển thị ngày bắt đầu hiện tại

            DateTime startDate;
            if (DateTime.TryParseExact(startDateString, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out startDate))
            {
                project.StartDate = startDate; // Cập nhật ngày bắt đầu
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Lỗi", "Định dạng ngày không hợp lệ.", "OK");
                return;
            }

            var endDateString = await Application.Current.MainPage.DisplayPromptAsync(
                "Chỉnh Sửa Dự Án",
                "Nhập ngày kết thúc (dd/MM/yyyy):",
                "OK", "Hủy",
                project.EndDate.ToString("dd/MM/yyyy")); // Hiển thị ngày kết thúc hiện tại

            DateTime endDate;
            if (DateTime.TryParseExact(endDateString, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out endDate))
            {
                project.EndDate = endDate; // Cập nhật ngày kết thúc
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Lỗi", "Định dạng ngày không hợp lệ.", "OK");
                return;
            }

            // Tiến hành cập nhật dự án vào cơ sở dữ liệu
            _projectRepository.UpdateProject(project);
            LoadProjects(); // Tải lại danh sách dự án sau khi cập nhật
        }


        // Xóa dự án
        private void OnDeleteProject(int projectId)
        {
            _projectRepository.DeleteProject(projectId); // Xóa dự án theo ProjectId
            LoadProjects(); // Tải lại danh sách dự án sau khi xóa
        }

        // Điều hướng quay lại trang trước
        private async void OnGoBack()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}