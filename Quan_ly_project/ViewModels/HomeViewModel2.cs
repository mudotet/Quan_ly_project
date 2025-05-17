using Quan_ly_project.Data;
using Quan_ly_project.Models;
using Quan_ly_project.Views;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace Quan_ly_project.ViewModels
{
    public class HomeViewModel2 : BaseViewModel
    {
        public ICommand NavigateToProjectCommand { get; }
        public ICommand NavigateToProjectMemberCommand { get; }
        public ICommand NavigateToTaskCommand { get; }
        public ICommand LogoutCommand { get; }

        public HomeViewModel2()
        {
            // Khởi tạo các lệnh điều hướng
            NavigateToProjectCommand = new Command(OnNavigateToProject);
            NavigateToProjectMemberCommand = new Command(OnNavigateToProjectMember);
            NavigateToTaskCommand = new Command(OnNavigateToTask);
            LogoutCommand = new Command(OnLogout);
        }

        // Điều hướng đến trang quản lý dự án
        private async void OnNavigateToProject()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ProjectPage());
        }

        // Điều hướng đến trang quản lý thành viên dự án
        private async void OnNavigateToProjectMember()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ProjectMemberPage());
        }

        // Điều hướng đến trang quản lý nhiệm vụ
        private async void OnNavigateToTask()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new TaskPage());
        }

        // Đăng xuất và quay lại trang đăng nhập
        private async void OnLogout()
        {
            await Application.Current.MainPage.Navigation.PopToRootAsync();
        }
    }
}
