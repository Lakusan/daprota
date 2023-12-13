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
            // Services
            builder.Services.AddSingleton<Storage>();
            builder.Services.AddSingleton<Data>();
            // Pages
            builder.Services.AddTransient<CoursesPage>();
            builder.Services.AddTransient<CourseDetailsPage>();
            builder.Services.AddTransient<QuestionsPage>();
            builder.Services.AddTransient<ChangeUsernamePage>();
            builder.Services.AddTransient<LessonChatPage>();
            builder.Services.AddTransient<Debug>();
            // ViewModels
            builder.Services.AddTransient<VM_Courses>();
            builder.Services.AddTransient<VM_CourseDetails>();
            builder.Services.AddTransient<VM_Questions>();
            builder.Services.AddTransient<VM_Settings>();
            builder.Services.AddTransient<VM_Debug>();

            return builder.Build();
        }
    }
}
