using Quan_ly_project.ViewModels;

namespace Quan_ly_project.Views
{
    public partial class TaskPage : ContentPage
    {
        public TaskPage()
        {
            InitializeComponent();
            BindingContext = new TaskViewModel(); // Liên kết với ViewModel
        }
    }
}