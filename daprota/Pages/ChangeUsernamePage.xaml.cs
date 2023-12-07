using daprota.Models;
using daprota.ViewModels;

namespace daprota.Pages;

public partial class ChangeUsernamePage : ContentPage
{
    private VM_Settings _vm;
    private string tmpUsername;
    private bool resetProgress = false;
    public M_User currentUser { get; set; }


    public ChangeUsernamePage(VM_Settings vm)
    {
        InitializeComponent();
        BindingContext = vm;
        _vm = vm;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        // set current username
        currentUser = _vm.GetCurrentUserProfile();
        l_username.Text = currentUser.Username;
    }

    private void e_username_TextChanged(object sender, TextChangedEventArgs e)
    {
        // store new User Name in variable every time user changes something
        tmpUsername = e.NewTextValue;
    }

    private async void BtnChangeUsernameClicked(object sender, EventArgs e)
    {
        _vm.setNewUsername(tmpUsername);
        currentUser = _vm.GetCurrentUserProfile();
        l_username.Text = currentUser.Username;
    }

    private async void BtnResetClicked(object sender, EventArgs e)
    {
        bool userChoice = await DisplayAlert("Alert", "Reset User Profile", "Proceed", "Cancel");
        resetProgress = userChoice;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        e_username.Text="";
        if (resetProgress) { 
            _vm.ResetUserProfile();
        }
    }
}
