using Quan_ly_project.Data;
using Quan_ly_project.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace Quan_ly_project.ViewModels
{
    public class TaskViewModel : BaseViewModel
    {
        private readonly TaskRepository _taskRepository;
        private ObservableCollection<ProjectTask> _tasks;
        private int _currentProjectId = 1; // ProjectId mặc định là 1

        public ObservableCollection<ProjectTask> Tasks
        {
            get => _tasks;
            set => SetProperty(ref _tasks, value);
        }

        public ICommand GoBackCommand { get; }
        public ICommand AddTaskCommand { get; }
        public ICommand EditTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }
        public ICommand DeleteAllTasksCommand { get; }

        public TaskViewModel()
        {
            _taskRepository = new TaskRepository();
            Tasks = new ObservableCollection<ProjectTask>();

            AddTaskCommand = new Command(OnAddTask);
            EditTaskCommand = new Command<ProjectTask>(OnEditTask);
            DeleteTaskCommand = new Command<int>(OnDeleteTask);
            DeleteAllTasksCommand = new Command(OnDeleteAllTasks);
            GoBackCommand = new Command(OnGoBack);

            LoadTasksByProject(_currentProjectId); // Load nhiệm vụ khi khởi tạo
        }

        // Load danh sách nhiệm vụ của dự án
        private void LoadTasksByProject(int projectId)
        {
            var tasks = _taskRepository.GetTasksByProjectId(projectId);
            Tasks.Clear();
            foreach (var task in tasks)
            {
                Tasks.Add(task);
            }
        }

        // Thêm nhiệm vụ
        private async void OnAddTask()
        {
            var name = await Application.Current.MainPage.DisplayPromptAsync("Thêm Nhiệm Vụ", "Nhập tên nhiệm vụ:", "OK", "Hủy", null, -1, Keyboard.Default, "");
            if (string.IsNullOrWhiteSpace(name)) return;

            var description = await Application.Current.MainPage.DisplayPromptAsync("Thêm Nhiệm Vụ", "Nhập mô tả:", "OK", "Hủy", null, -1, Keyboard.Default, "");
            if (description == null) description = "";

            var status = await Application.Current.MainPage.DisplayPromptAsync("Thêm Nhiệm Vụ", "Nhập trạng thái (ví dụ: Pending, In Progress, Completed):", "OK", "Hủy", null, -1, Keyboard.Default, "Pending");
            if (string.IsNullOrWhiteSpace(status)) status = "Pending";

            var deadlineString = await Application.Current.MainPage.DisplayPromptAsync("Thêm Nhiệm Vụ", "Nhập deadline (dd/MM/yyyy):", "OK", "Hủy", null, -1, Keyboard.Default, DateTime.Now.AddDays(7).ToString("dd/MM/yyyy"));
            if (!DateTime.TryParseExact(deadlineString, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime deadline))
            {
                await Application.Current.MainPage.DisplayAlert("Lỗi", "Định dạng ngày không hợp lệ.", "OK");
                return;
            }

            var progressPercentString = await Application.Current.MainPage.DisplayPromptAsync("Thêm Nhiệm Vụ", "Nhập tiến độ (%):", "OK", "Hủy", null, -1, Keyboard.Numeric, "0");
            if (!int.TryParse(progressPercentString, out int progressPercent) || progressPercent < 0 || progressPercent > 100)
            {
                await Application.Current.MainPage.DisplayAlert("Lỗi", "Tiến độ phải là số từ 0 đến 100.", "OK");
                return;
            }

            var newTask = new ProjectTask
            {
                ProjectId = _currentProjectId,
                Name = name,
                Description = description,
                Status = status,
                ProgressPercent = progressPercent,
                AssigneeId = 1,
                Deadline = deadline,
                CreatedAt = DateTime.Now
            };

            _taskRepository.AddTask(newTask);
            LoadTasksByProject(_currentProjectId); // Tải lại danh sách nhiệm vụ sau khi thêm
        }

        // Chỉnh sửa nhiệm vụ
        private async void OnEditTask(ProjectTask taskToEdit)
        {
            if (taskToEdit == null) return;

            // Hiển thị các cửa sổ nhập liệu cho việc chỉnh sửa nhiệm vụ
            var name = await Application.Current.MainPage.DisplayPromptAsync("Chỉnh Sửa Nhiệm Vụ", "Tên:", "OK", "Hủy", taskToEdit.Name);
            if (name != null) taskToEdit.Name = name;

            var description = await Application.Current.MainPage.DisplayPromptAsync("Chỉnh Sửa Nhiệm Vụ", "Mô tả:", "OK", "Hủy", taskToEdit.Description);
            if (description != null) taskToEdit.Description = description;

            var status = await Application.Current.MainPage.DisplayPromptAsync("Chỉnh Sửa Nhiệm Vụ", "Trạng thái:", "OK", "Hủy", taskToEdit.Status);
            if (status != null) taskToEdit.Status = status;

            var deadlineString = await Application.Current.MainPage.DisplayPromptAsync("Chỉnh Sửa Nhiệm Vụ", "Deadline (dd/MM/yyyy):", "OK", "Hủy", taskToEdit.Deadline.ToString("dd/MM/yyyy"));
            if (DateTime.TryParseExact(deadlineString, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime deadline))
            {
                taskToEdit.Deadline = deadline;
            }

            var progressPercentString = await Application.Current.MainPage.DisplayPromptAsync("Chỉnh Sửa Nhiệm Vụ", "Tiến độ (%):", "OK", "Hủy", taskToEdit.ProgressPercent.ToString());
            if (int.TryParse(progressPercentString, out int progressPercent) && progressPercent >= 0 && progressPercent <= 100)
            {
                taskToEdit.ProgressPercent = progressPercent;
            }

            _taskRepository.UpdateTask(taskToEdit);  // Cập nhật vào DB
            LoadTasksByProject(_currentProjectId);  // Tải lại danh sách nhiệm vụ
        }

        // Xóa nhiệm vụ
        private async void OnDeleteTask(int taskId)
        {
            bool result = await Application.Current.MainPage.DisplayAlert("Xác nhận", "Bạn có chắc chắn muốn xóa nhiệm vụ này?", "Có", "Không");
            if (result)
            {
                _taskRepository.DeleteTask(taskId);  // Xóa nhiệm vụ theo TaskId
                LoadTasksByProject(_currentProjectId);  // Tải lại danh sách nhiệm vụ
            }
        }



        // Xóa tất cả các nhiệm vụ
        private async void OnDeleteAllTasks()
        {
            bool result = await Application.Current.MainPage.DisplayAlert("Cảnh báo", "Bạn có chắc chắn muốn xóa TẤT CẢ các nhiệm vụ?", "Có", "Không");
            if (result)
            {
                _taskRepository.DeleteAllTasks();
                LoadTasksByProject(_currentProjectId);
            }
        }

        // Điều hướng quay lại
        private async void OnGoBack()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}