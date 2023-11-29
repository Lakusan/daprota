using daprota.Pages;
using daprota.Services;
using daprota.ViewModels;
using Microsoft.Extensions.Logging;

namespace daprota
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

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            // Pages
            builder.Services.AddSingleton<CoursesPage>();
            builder.Services.AddTransient<CourseDetailsPage>();
            // ViewModels
            builder.Services.AddSingleton<VM_Courses>();
            builder.Services.AddTransient<VM_CourseDetails>();
            // Services
            builder.Services.AddSingleton<Storage>();

            return builder.Build();
        }
    }
}
