namespace daprota.Models
{
    public class M_Question
    {
        public int CourseId { get; set; }
        public List<M_Answer> Answers { get; set; }
        public string QuestionText { get; set; }

        public M_Question()
        {
            Answers = new List<M_Answer>();
        }
    }
}
