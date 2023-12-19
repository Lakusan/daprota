using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using daprota.Models;
using daprota.Pages;
using daprota.Services;

namespace daprota.ViewModels
{
    public partial class VM_Settings : ObservableObject
    {

        private Storage _storage;
        private Data _data;

        public M_User CurrentUser { get; set; }
        [ObservableProperty]
        public string userName;
        
        public VM_Settings(Storage s, Data d) { 
            _storage = s;
            _data = d;
            CurrentUser = new();
        }

        public M_User GetCurrentUserProfile()
        {
            return _data.GetUser();
        }

        public void setNewUsername(string username)
        {
            CurrentUser = _data.SetUserName(username);
            _storage.SetUserDataToPrefs(CurrentUser);
        }

        public void ResetUserProfile()
        {
            M_User defaultUser = Data.DefaultUserProfile;
            _storage.SetUserDataToPrefs<M_User>(defaultUser);

            Data.UserData = null;
            CurrentUser = _data.GetUser();
        }

        [RelayCommand]
        public async Task GoBack()
        {
            await Shell.Current.GoToAsync($"{nameof(CoursesPage)}");
        }
    }
}
