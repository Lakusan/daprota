namespace daprota.Models
{
    public class M_Answer
    {
        public int Id { get; set; }
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }
        public string ExplainationText { get; set; }
    }
}
