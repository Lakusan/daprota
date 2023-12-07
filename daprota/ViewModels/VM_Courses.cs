using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using daprota.Models;
using daprota.Pages;
using daprota.Services;

namespace daprota.ViewModels
{
    public partial class VM_Courses : ObservableObject
    {
        public List<M_Course> Courses { get; set; }
        private Storage _storage;
        public M_User currentUser { get; set; }

        public VM_Courses(Storage s)
        {
            _storage = s;
            currentUser = App._userData;
        }

        public M_User GetCurrentUserProfile()
        {
            return App._userData;
        }

        public async Task LoadDataAsync()
        {
            Courses = await _storage.ReadEmbeddedXML<List<M_Course>>("courses.xml"); 
        }
        [RelayCommand]
        async Task Tap(int id)
        {
            // Send Dict of filtered Courses, if not filtered use _courses
            await Shell.Current.GoToAsync($"{nameof(CourseDetailsPage)}?Id={id}");
        }

        public List<M_Course> GetFilteredItems(string title)
        {
            return Courses.Where(course => course.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // create Progress of Current course
        //
        // - 4 lessons max, 1 lesson 25% -> maxLesson/LessonsCompleted Count APP.MAX_LESSONS_PER_COURSE
        private float GetCurrentCourseProgress()
        {
            float progress = 0f;
            return progress;
        }

        private int GetCurrentCourseId()
        {
            return Courses.Count;
        }

        // Get Progress Data from User profile to tell the view whats goin on

        // view must have prograss bars on each course card. 

        // how to define the main course or my course List -> Last one is active. Then only data to done lesson. Next lesson is active one. 1 course => 4 lessons

    }
}
