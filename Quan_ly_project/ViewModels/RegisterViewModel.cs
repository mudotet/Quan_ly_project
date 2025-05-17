using Quan_ly_project.Data;
using Quan_ly_project.Models;
using Quan_ly_project.Views;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace Quan_ly_project.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private string _username;
        private string _password;
        private string _email;

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

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public ICommand RegisterCommand { get; }
        public ICommand NavigateToLoginCommand { get; }

        private readonly UserRepository _userRepository;

        public RegisterViewModel()
        {
            _userRepository = new UserRepository();
            RegisterCommand = new Command(OnRegister);
            NavigateToLoginCommand = new Command(OnNavigateToLogin);
        }

        private async void OnRegister()
        {
            var user = new User
            {
                Username = Username,
                Password = Password,
                FullName = "New User", // You can set this dynamically
                Email = Email,
                Role = "User", // Default role is User
                CreatedAt = DateTime.Now
            };

            _userRepository.AddUser(user);
            await Application.Current.MainPage.DisplayAlert("Đăng ký thành công", "Tài khoản đã được tạo!", "OK");

            // Điều hướng về trang đăng nhập sau khi đăng ký thành công
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }

        private async void OnNavigateToLogin()
        {
            // Điều hướng đến trang đăng nhập từ trang đăng ký
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }
    }
}