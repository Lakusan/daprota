using CommunityToolkit.Mvvm.ComponentModel;

namespace daprota.Models
{
    public partial class M_Course : ObservableObject
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required string Description_long { get; set; }
        public required string Description_short { get; set; }
        public required string Image { get; set; }
        public required string Category { get; set; }
        public List<M_Question> Questions { get; set; }
    }
}
