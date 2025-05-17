using Quan_ly_project.ViewModels;

namespace Quan_ly_project.Views
{
    public partial class ProjectPage : ContentPage
    {
        public ProjectPage()
        {
            InitializeComponent();
            BindingContext = new ProjectViewModel(); // Liên kết với ViewModel
        }
    }
}