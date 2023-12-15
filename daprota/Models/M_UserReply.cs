namespace daprota.Models
{
    public class M_UserResponse
    {
        public int UserResponseId { get; set; }
        public int ConversationId {  get; set; }
        public string PositiveResponse { get; set; }
        public string NegativeResponse { get; set; }
    }
}
