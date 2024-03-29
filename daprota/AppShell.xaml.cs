﻿using daprota.Pages;

namespace daprota
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(CoursesPage), typeof(CoursesPage));
            Routing.RegisterRoute(nameof(CourseDetailsPage), typeof(CourseDetailsPage));
            Routing.RegisterRoute(nameof(QuestionsPage), typeof(QuestionsPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
            Routing.RegisterRoute(nameof(Debug), typeof(Debug));
            Routing.RegisterRoute(nameof(IntroPage), typeof(IntroPage));
        }
    }
}
