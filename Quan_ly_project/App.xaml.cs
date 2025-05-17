using Quan_ly_project.Views;
namespace Quan_ly_project
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage()); // Đảm bảo sử dụng NavigationPage
        }

    }
}