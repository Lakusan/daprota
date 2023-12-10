
namespace daprota.Models
{
    public class M_User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public DateOnly LastActivityDate { get; set; }
        public bool FirstStart { get; set; }
        public int ActiveCourseId { get; set; }
        public int ActiveLessionId { get; set; }
    }
}



