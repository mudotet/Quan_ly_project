using System.Diagnostics;
using Quan_ly_project.ViewModels;

namespace Quan_ly_project.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            BindingContext = new HomeViewModel(); 
        }
    }
}