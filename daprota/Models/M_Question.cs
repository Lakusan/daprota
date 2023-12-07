namespace daprota.Models
{
    public class M_Question
    {
        public string qText { get; set; }
        public List<M_Answer> qAnswers { get; set; }
        public string text { get; set; }

        public M_Question()
        {
            qAnswers = new List<M_Answer>();
        }
    }
}
