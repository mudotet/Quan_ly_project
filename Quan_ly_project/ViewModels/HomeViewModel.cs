using Quan_ly_project.Data;
using Quan_ly_project.Models;
using Quan_ly_project.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace Quan_ly_project.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public ObservableCollection<Project> Projects { get; }
        private readonly ProjectRepository _projectRepository;

        public ICommand LogoutCommand { get; }
        public ICommand NavigateToProjectCommand { get; }
        public ICommand NavigateToProjectMemberCommand { get; }
        public ICommand NavigateToTaskCommand { get; }

        public HomeViewModel()
        {
            Projects = new ObservableCollection<Project>();
            _projectRepository = new ProjectRepository();

            LogoutCommand = new Command(OnLogout);
            NavigateToProjectCommand = new Command(OnNavigateToProject);
            NavigateToProjectMemberCommand = new Command(OnNavigateToProjectMember);
            NavigateToTaskCommand = new Command(OnNavigateToTask);

            LoadProjects();
        }

        private void LoadProjects()
        {
            var projects = _projectRepository.GetAllProjects();
            Projects.Clear();  // Clear the existing list before loading new data
            foreach (var project in projects)
            {
                Projects.Add(project);
            }
        }

        private async void OnLogout()
        {
            // Đăng xuất và điều hướng về trang LoginPage
            await Application.Current.MainPage.Navigation.PopToRootAsync(); // Quay về màn hình đăng nhập
        }

        private async void OnNavigateToProject()
        {
            // Điều hướng đến trang Project
            await Application.Current.MainPage.Navigation.PushAsync(new ProjectPage());
        }

        private async void OnNavigateToProjectMember()
        {
            // Điều hướng đến trang ProjectMember
            await Application.Current.MainPage.Navigation.PushAsync(new ProjectMemberPage());
        }

        private async void OnNavigateToTask()
        {
            // Điều hướng đến trang Task
            await Application.Current.MainPage.Navigation.PushAsync(new TaskPage());
        }
    }
}
