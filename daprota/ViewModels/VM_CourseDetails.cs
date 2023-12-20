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
        [ObservableProperty]
        public List<M_Lesson> lessons;

        [ObservableProperty]
        public M_User user;
        
        public string DebugText { get; set; }

        private Data _data;

        public VM_CourseDetails(Data d)
        {
            _data = d;
            CourseDetails = new();
        }

        public async Task LoadData()
        {
            _data.GetUser();
            User = Data.UserData;
            await _data.GetCurrentCourse();
            CurrentCourse = Data.CurrentCourse;
            CourseDetails = await _data.GenerateAsyncCourseDetails(CurrentCourse);
            GenerateLessonsList();
            SetLessonsDone();
        }
        public void GetCourseProgressionPercentage()
        {
            CurrentCourseProgressText = _data.GetCourseProgressionPercentage();
        }
        public void GetCourseProgressionFloat()
        {
            CurrentCourseProgressBar = _data.GetCourseProgressionFloat();
        }
        public void SetCurrentCourseLessonProgress()
        {
            User = _data.GetUser();
            if (User.ActiveLessionId >= 1)
            {
                CurrentCourseLessonProgress = User.ActiveLessionId;
            }
            else
            {
                CurrentCourseLessonProgress = 0;
            }
        }
        public void SetLessonsDone()
        {
            User = _data.GetUser();
            int currentLessonId = User.ActiveLessionId;
            foreach(M_Lesson lesson in lessons)
            {
                if(lesson.Id < currentLessonId)
                {
                    lesson.IsDone = true;
                    lesson.StatusImage = "lesson_tick_true.png";
                }
            }
        }
        public void GenerateLessonsList()
        {
            Lessons = CourseDetails.Lessons.FindAll(l => l.Id <= User.ActiveLessionId );
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
                    Data.SelectedLessonId = lesson.Id;
                    await Shell.Current.GoToAsync($"{nameof(IntroPage)}");
                    break;
                case 1:
                    // Chat2
                    Data.SelectedLessonId = lesson.Id;
                    await Shell.Current.GoToAsync($"{nameof(IntroPage)}");
                    break;
                case 2:
                    // Conversation Q&A
                    Data.SelectedLessonId = lesson.Id;
                    await Shell.Current.GoToAsync($"{nameof(IntroPage)}");
                    break;
                case 3:
                    //Quiz
                    Data.SelectedLessonId = lesson.Id;
                    await Shell.Current.GoToAsync($"{nameof(QuestionsPage)}");
                    break;
                default:
                    break;
            }
        }
    }
}
