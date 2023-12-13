namespace daprota.Models
{
    public class M_CourseDetails
    {
        public M_Course Course { get; set; }
        public List<M_Lesson> Lessons { get; set; }
        public List<M_Question> Questions { get; set; }
        public List<M_Answer> Answers { get; set; }
    }
}
