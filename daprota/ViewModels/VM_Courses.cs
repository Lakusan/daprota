using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using daprota.Models;
using daprota.Pages;
using daprota.Services;

namespace daprota.ViewModels
{
    public partial class VM_Courses : ObservableObject
    {
        private Storage _storage;

        [ObservableProperty]
        public List<M_Course> courses;

        [ObservableProperty]
        public M_User currentUser;

        [ObservableProperty]
        public M_CurrentCourse currentCourse;

        [ObservableProperty]
        public float currentCourseProgressBar;

        [ObservableProperty]
        public int currentCourseProgressText;

        [ObservableProperty]
        public int currentCourseLessonProgress;
        
        public VM_Courses(Storage s)
        {
            _storage = s;
            currentUser = GetCurrentUserProfile();
        }


        // Current Course
        public async Task GetCurrentCourseData()
        {
            try
            {
                CurrentCourse = new M_CurrentCourse()
                {
                    CurrentCurseId = CurrentUser.ActiveCourseId,
                    CurrentLessonId = CurrentUser.ActiveLessionId,
                    CurrentCourseTitle = GetCourseTitleFromCourseId(CurrentUser.ActiveCourseId),
                    Image = GetCourseImageFromCourseId(CurrentUser.ActiveCourseId),
                };
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("ERROR ", $"DATA: {CurrentCourse}\n {ex}", "ok");
            }
        }
        public string GetCourseTitleFromCourseId(int courseId)
        {
            string? s = string.Empty;

            s = Courses.Where(course => course.Id == courseId)
                                  .Select(course => course.Title)
                                   .FirstOrDefault();
            if (s != null)
            {
                return s;
            }
            return "Error: not found";

        }
        public string GetCourseImageFromCourseId(int courseId)
        {
            string? s = Courses.Where(course => course.Id == courseId)
                               .Select(course => course.Image)
                               .FirstOrDefault();
            if (s != null)
            {
                return s;
            }
            return "Error: not found";
        }
        public int GetCurrentCourseId()
        {
            return CurrentUser.ActiveCourseId;  
        }
        public void SetCourseProgressionPercentage(int currentLessonId)
        {
            int value = 0;
            switch (currentLessonId)
            {
                case 0:
                    value = 1;
                    break;
                case 1:
                    value = 25;
                    break;
                case 2:
                    value = 50;
                    break;
                case 3:
                    value = 75;
                    break;
                case 5:
                    value = 100;
                    break;
                default:
                    value = 0;
                    break;
            }
            CurrentCourseProgressText = value;
        }
        public void SetCourseProgressionFloat(int currentLessonId)
        {
            float value = 0f;
            switch (currentLessonId)
            {
                case 0:
                    value = 0.1f;
                    break;
                case 1:
                    value = 0.25f;
                    break;
                case 2:
                    value = .5f;
                    break;
                case 3:
                    value = .75f;
                    break;
                case 5:
                    value = 1f;
                    break;
                default:
                    value = 0f;
                    break;
            }
            CurrentCourseProgressBar = value;
        }
        public void SetCurrentCourseLessonProgress(int currentLessonId)
        {
            if (currentLessonId >=1)
            {
                CurrentCourseLessonProgress = currentLessonId -1;
            }
            else
            {
                CurrentCourseLessonProgress =  0;
            }
        }

        // User Profile 
        public M_User GetCurrentUserProfile()
        {
            return App._userData;
        }
        // Data
        public async Task LoadDataAsync()
        {
            Courses = await _storage.ReadEmbeddedXML<List<M_Course>>("courses.xml");
            await GetCurrentCourseData();
        }
        // Interactions
        [RelayCommand]
        public async Task MyCourseTapped()
        {
            //await Shell.Current.GoToAsync($"{nameof(CourseDetailsPage)}?Id={CurrentCourse.CurrentCurseId}");
            // find correct course Data from courses
            M_Course? myCourse = Courses.FirstOrDefault(Courses => Courses.Id == CurrentCourse.CurrentCurseId);
            if (myCourse != null)
            {
                await Shell.Current.GoToAsync($"{nameof(CourseDetailsPage)}",
                    new Dictionary<string, object>
                    {
                        {"CurrentCourse", myCourse },
                    });
            }
        }
        [RelayCommand]
        public async Task CourseTapped(int Id)
        {
            // find tapped course by CourseId
            M_Course? courseTapped = Courses.FirstOrDefault(Courses => Courses.Id == Id);
                if(courseTapped != null)
                {
                    await Shell.Current.GoToAsync($"{nameof(CourseDetailsPage)}", new Dictionary<string, object>
                {
                    {"CurrentCourse", courseTapped},
                });
            }
        }
        public List<M_Course> GetFilteredItems(string title)
        {
            return Courses.Where(course => course.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}
