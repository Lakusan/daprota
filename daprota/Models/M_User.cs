
namespace daprota.Models
{
    public class M_User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public DateOnly LastActivityDate { get; set; }
        public bool FirstStart { get; set; }
        public List<M_UserCorseProgess> CourseProgression { get; set; }
    }
}



