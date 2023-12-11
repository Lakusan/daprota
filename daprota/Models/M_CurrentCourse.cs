using Android.Database;
using CommunityToolkit.Mvvm.ComponentModel;

namespace daprota.Models
{
    public class M_CurrentCourse : ObservableObject
    {
        public int CurrentCurseId { get; set; }
        public int CurrentLessonId { get; set; }
        public string CurrentCourseTitle { get; set; }
        public string Image { get; set; }
        
    }
}
