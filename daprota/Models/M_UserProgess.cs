namespace daprota.Models
{
    class M_UserProgess
    {
        public required int CurrentCourseId { get; set; }
        public required int CurrentLessonId{ get; set; }
        public required int CurrentCourseProgress { get; set; }
        public required int CurrentLessonProgress { get; set; }
        public required List<int> LessonsCompleted { get; set; }
    }
}


