namespace daprota.Models
{
    public class M_Conversation
    {
        public int ConversationId { get; set; }
        public List<M_BotMsg> BotMsgList { get; set; }
        public List<M_UserResponse> UserResponseList { get; set; }
    }
}
