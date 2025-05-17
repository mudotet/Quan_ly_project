using Quan_ly_project.Data;
using Quan_ly_project.Models;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Quan_ly_project.Views;

namespace Quan_ly_project.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _username;
        private string _password;

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public ICommand LoginCommand { get; }
        public ICommand NavigateToRegisterCommand { get; }

        private readonly UserRepository _userRepository;

        public LoginViewModel()
        {
            _userRepository = new UserRepository();
            LoginCommand = new Command(OnLogin);
            NavigateToRegisterCommand = new Command(OnNavigateToRegister);
        }

        private async void OnLogin()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Lỗi", "Vui lòng nhập tên đăng nhập và mật khẩu", "OK");
                return;
            }

            await Application.Current.MainPage.Navigation.PushAsync(new HomePage2());
        }

        private async void OnNavigateToRegister()
        {
            // Điều hướng đến trang đăng ký
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }
    }
}
