using System.Diagnostics;
using Quan_ly_project.ViewModels;

namespace Quan_ly_project.Views
{
    public partial class HomePage2 : ContentPage
    {
        public HomePage2()
        {
            InitializeComponent();
            BindingContext = new HomeViewModel2();
        }
    }
}