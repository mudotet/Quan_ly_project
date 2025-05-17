using Quan_ly_project.ViewModels;

namespace Quan_ly_project.Views
{
    public partial class ProjectMemberPage : ContentPage
    {
        public ProjectMemberPage()
        {
            InitializeComponent();
            BindingContext = new ProjectMemberViewModel(); // Liên kết với ViewModel
        }
    }
}