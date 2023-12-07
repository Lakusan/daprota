using daprota.Pages;

namespace daprota
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(CourseDetailsPage), typeof(CourseDetailsPage));
            Routing.RegisterRoute(nameof(QuestionsPage), typeof(QuestionsPage));
            Routing.RegisterRoute(nameof(ChangeUsernamePage), typeof(ChangeUsernamePage));
        }
    }
}
