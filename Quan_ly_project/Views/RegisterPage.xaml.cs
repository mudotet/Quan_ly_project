using Quan_ly_project.ViewModels;

namespace Quan_ly_project.Views
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = new RegisterViewModel(); // Liên kết với ViewModel
        }
    }
}