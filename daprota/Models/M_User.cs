namespace daprota.Models
{
    class M_User
    {
        public required int UserId { get; set; }
        public required string Username { get; set; }
        public required DateTime LastActivityDate { get; set; }
        public required List<int> CoursesCompleted { get; set; }
    }
}



