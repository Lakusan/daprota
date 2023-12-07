using CommunityToolkit.Mvvm.ComponentModel;
using daprota.Models;
using daprota.Services;

namespace daprota.ViewModels
{
    public partial class VM_Settings : ObservableObject
    {

        private Storage _storage;
        public M_User currentUser {  get; set; }
        

        public VM_Settings(Storage s) { 
            _storage = s;
            currentUser = App._userData;
        }

        public M_User GetCurrentUserProfile()
        {
            return App._userData;
        }

        public void setNewUsername(string username)
        {
            // set new username in user model obj
            App._userData.Username= username;
            // store new User object in Prefs
            _storage.SetUserDataToPrefs(GetCurrentUserProfile());
        }

        public void ResetUserProfile()
        {
            // set user profile to default -> Reset user progress
            _storage.SetUserDataToPrefs<M_User>(App._defaulUserProfile);
            App._userData = App._defaulUserProfile;
            currentUser = App._userData;
        }

    }
}
