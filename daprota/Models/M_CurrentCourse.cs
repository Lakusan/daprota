using CommunityToolkit.Mvvm.ComponentModel;

namespace daprota.Models
{
    public class M_CurrentCourse : ObservableObject
    {
        public int CurrentCurseId { get; set; }
        public int CurrentLessonId { get; set; }
        public string CurrentCourseTitle { get; set; }
        public string Image { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }

    }
}
