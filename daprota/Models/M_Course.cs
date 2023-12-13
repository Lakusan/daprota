using CommunityToolkit.Mvvm.ComponentModel;

namespace daprota.Models
{
    public partial class M_Course : ObservableObject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description_long { get; set; }
        public string Description_short { get; set; }
        public string Image { get; set; }
        public string Category { get; set; }
    }
}
