using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using daprota.Models;
using daprota.Pages;
using daprota.Services;

namespace daprota.ViewModels
{
    [QueryProperty("CurrentCourse","CurrentCourse")]
    [QueryProperty("CurrentCourseProgressBar", "CurrentCourseProgressBar")]
    [QueryProperty("CurrentCourseProgressText", "CurrentCourseProgressText")]
    [QueryProperty("CurrentCourseLessonProgress", "CurrentCourseLessonProgress")]
    public partial class VM_CourseDetails : ObservableObject
    {
        [ObservableProperty]
        public M_Course currentCourse;
        
        [ObservableProperty]
        public float currentCourseProgressBar;

        [ObservableProperty]
        public int currentCourseProgressText;

        [ObservableProperty]
        public int currentCourseLessonProgress;

        [ObservableProperty]
        public M_CourseDetails courseDetails;
        
        public string DebugText { get; set; }

        private Data _data;

        public VM_CourseDetails(Data d)
        {
            _data = d;
            CourseDetails = new();
        }

        [RelayCommand]
        public async Task GoBack()
        {
            await Shell.Current.GoToAsync($"{nameof(CoursesPage)}");
        }

        public async Task GetLessonData()
        {
            CourseDetails = await _data.GenerateAsyncCourseDetails(CurrentCourse);
        }
    }
}
