using daprota.ViewModels;

namespace daprota.Pages;

public partial class SettingsPage : ContentPage
{
    private VM_Settings _vm;
    private string tmpUsername;
    private bool resetProgress = false;
    private string username = "";

    public SettingsPage(VM_Settings vm)
    {
        InitializeComponent();
        BindingContext = vm;
        _vm = vm;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        // set current username
        username = "Hello " + _vm.GetCurrentUserProfile().Username;
        l_username.Text= username;
    }

    private void e_username_TextChanged(object sender, TextChangedEventArgs e)
    {
        tmpUsername = e.NewTextValue;
    }

    private async void BtnChangeUsernameClicked(object sender, EventArgs e)
    {
        _vm.setNewUsername(tmpUsername);
        await DisplayAlert("Username Changed to: ",tmpUsername, "Ok");
        username = "Hello " + _vm.GetCurrentUserProfile().Username;
        l_username.Text = username;
    }

    private async void BtnResetClicked(object sender, EventArgs e)
    {
        resetProgress = await DisplayAlert("Caution!", "Reset User Profile", "Proceed", "Cancel");
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        if (resetProgress) { 
            _vm.ResetUserProfile();
        }
        resetProgress = false;
    }
    private async void Back_Tapped(object sender, TappedEventArgs e)
    {
        await _vm.GoBack();
    }
}
