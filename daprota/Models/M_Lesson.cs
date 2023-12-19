namespace daprota.Models
{
    public class M_Lesson
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool IsDone { get; set; }
        public string StatusImage { get; set; }
    }
}
