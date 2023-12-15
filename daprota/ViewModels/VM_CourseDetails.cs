using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using daprota.Models;
using daprota.Pages;
using daprota.Services;

namespace daprota.ViewModels
{
    public partial class VM_CourseDetails : ObservableObject
    {
        [ObservableProperty]
        public M_CurrentCourse currentCourse;
        
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

        public async Task LoadData()
        {
            await _data.GetCurrentCourse();
            CurrentCourse = Data.CurrentCourse;
            CourseDetails = await _data.GenerateAsyncCourseDetails(CurrentCourse);
        }

        [RelayCommand]
        public async Task GoBack()
        {
            await Shell.Current.GoToAsync($"{nameof(CoursesPage)}");
        }

        [RelayCommand]
        public async Task GoToLesson(M_Lesson lesson)
        {
            switch (lesson.Id)
            {
                case 0:
                    await Shell.Current.GoToAsync($"{nameof(IntroPage)}",
                    new Dictionary<string, object>
                    {
                        {"CurrentCourse", CurrentCourse },
                        {"CurrentCourseProgressBar", CurrentCourseProgressBar },
                        {"CurrentCourseProgressText", CurrentCourseProgressText },
                        {"CurrentCourseLessonProgress", CurrentCourseLessonProgress },
                    });
                    break;
                case 1:
                    // Connect Words
                    break;
                case 2:
                    // Conversation
                    break;
                case 3:
                    //Quiz
                    break;
                default:
                    break;
            }
        }
    }
}
