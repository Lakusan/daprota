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
        private Data _data;

        [ObservableProperty]
        public M_User user;

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
        
        public VM_Courses(Storage s, Data d)
        {
            _storage = s;
            _data = d;
            CurrentUser = _data.GetUser();
        }
        public async Task GetCurrentCourseData()
        {
            await _data.GetCurrentCourse();
            CurrentCourse = Data.CurrentCourse;
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
            if (User.ActiveLessionId>=1)
            {
                CurrentCourseLessonProgress = User.ActiveLessionId;
            }
            else
            {
                CurrentCourseLessonProgress =  0;
            }
        }
        // User Profile 
        public M_User GetCurrentUserProfile()
        {
            return _data.GetUser();
        }
        // Data
        public async Task LoadDataAsync()
        {
            // filter available courses
            CurrentUser = GetCurrentUserProfile();
            Courses = await _data.GetCourses();
            await GetCurrentCourseData();
        }
        // Interactions
        [RelayCommand]
        public async Task MyCourseTapped()
        {
            await Shell.Current.GoToAsync($"{nameof(CourseDetailsPage)}");
            //await Shell.Current.GoToAsync($"{nameof(CourseDetailsPage)}?Id={CurrentCourse.CurrentCurseId}");
            // find correct course Data from courses
            //M_Course? myCourse = Courses.FirstOrDefault(Courses => Courses.Id == CurrentCourse.CurrentCurseId);
            //if (myCourse != null)
            //{
            //    await Shell.Current.GoToAsync($"{nameof(CourseDetailsPage)}",
            //        new Dictionary<string, object>
            //        {
            //            {"CurrentCourse", myCourse },
            //            {"CurrentCourseProgressBar", CurrentCourseProgressBar },
            //            {"CurrentCourseProgressText", CurrentCourseProgressText },
            //            {"CurrentCourseLessonProgress", CurrentCourseLessonProgress },
            //        });
            //}
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
                    {"CurrentCourseProgressBar", CurrentCourseProgressBar },
                    {"CurrentCourseProgressText", CurrentCourseProgressText },
                    {"CurrentCourseLessonProgress", CurrentCourseLessonProgress },
                });
            }
        }
        [RelayCommand]
        public async Task OtherCourseTapped(int Id)
        {
            // find tapped course by CourseId
            M_Course courseTapped = Courses.First(Courses => Courses.Id == Id);
            if (courseTapped != null)
            {
                await Shell.Current.GoToAsync($"{nameof(CourseDetailsPage)}");
                Data.LastCourse = courseTapped;
                
            }
        }

        public List<M_Course> GetFilteredItems(string title)
        {
            return Courses.Where(course => course.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        [RelayCommand]
        public async Task SettingsTapped()
        {
            await Shell.Current.GoToAsync($"{nameof(SettingsPage)}");
        }
    }
}
