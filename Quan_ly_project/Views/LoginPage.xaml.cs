using Quan_ly_project.ViewModels;

namespace Quan_ly_project.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel(); 
        }
    }
}
