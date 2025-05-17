using Quan_ly_project.ViewModels;
using Quan_ly_project.Services;

namespace Quan_ly_project
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Đăng ký ViewModels
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<RegisterViewModel>();
            builder.Services.AddSingleton<HomeViewModel>();
            builder.Services.AddSingleton<UserViewModel>();

            // Đăng ký các dịch vụ
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<PermissionService>();

            return builder.Build();
        }
    }
}