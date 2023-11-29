using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace daprota.ViewModels
{
    [QueryProperty("Id", "Id")]
    public partial class VM_CourseDetails : ObservableObject
    {
        [ObservableProperty]
        public int id;

        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
