namespace daprota.Models
{
    public class M_BotMsg
    {
        public int BotMsgId { get; set; }
        public int ConversationId { get; set; }
        public string BotName { get; set; }
        public string Statement { get; set; }   
        public string Explaination { get; set; }
    }
}
