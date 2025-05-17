using Quan_ly_project.Data;
using Quan_ly_project.Models;
using System.Windows.Input;
using Microsoft.Maui.Controls;  // Đúng cho .NET MAUI


namespace Quan_ly_project.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        private string _fullName;
        private string _email;
        private string _role;

        public string FullName
        {
            get => _fullName;
            set => SetProperty(ref _fullName, value);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Role
        {
            get => _role;
            set => SetProperty(ref _role, value);
        }

        public ICommand UpdateUserCommand { get; }

        private readonly UserRepository _userRepository;

        public UserViewModel()
        {
            _userRepository = new UserRepository();
            UpdateUserCommand = new Command(OnUpdateUser);
        }

        private async void OnUpdateUser()
        {
            var user = new User
            {
                FullName = FullName,
                Email = Email,
                Role = Role
            };

            _userRepository.UpdateUser(user);
            await Application.Current.MainPage.DisplayAlert("Cập nhật thành công", "Thông tin người dùng đã được cập nhật", "OK");
        }
    }
}