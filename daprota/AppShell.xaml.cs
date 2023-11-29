using daprota.Pages;

namespace daprota
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(CourseDetailsPage), typeof(CourseDetailsPage));
        }
    }
}
