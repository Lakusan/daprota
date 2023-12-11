using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using daprota.Models;

namespace daprota.ViewModels
{
    [QueryProperty("Id", "Id")]
    [QueryProperty("CurrentCourse","CurrentCourse")]
    public partial class VM_CourseDetails : ObservableObject
    {
        [ObservableProperty]
        public M_Course currentCourse;
        
        [ObservableProperty]
        public int courseId;

        [ObservableProperty]
        public string courseTitle;

        [ObservableProperty]
        public string courseImage;

        [ObservableProperty]
        public string id;


        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
