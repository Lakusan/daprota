using daprota.ViewModels;
using daprota.Models;
using Microsoft.Maui.Handlers;

namespace daprota.Pages;


//TODO: 
// Get user data -  Store user 
public partial class CoursesPage : ContentPage
{
    public List<M_Course> Courses { get; set; }
    private VM_Courses _vm;
    public M_User currentUser { get; set; }
    public CoursesPage(VM_Courses vm)
    {
        InitializeComponent();
        BindingContext = vm;
        _vm = vm;
        changeSearchBarIconColor();
        currentUser = _vm.GetCurrentUserProfile();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // set correct username
        currentUser = _vm.GetCurrentUserProfile();
        l_username.Text = currentUser.Username;

        //-> TODO: Load Course Data in App.xaml.cs once from xml ; runtime -> Obj
        await _vm.LoadDataAsync();
        // set courses to carousel
        CarV_Courses.ItemsSource = _vm.Courses;

        //-> TODO: Get Course Progress to be displayed on course cards

        // example use app Dictionary I/O
        //var pathToAppDictionary = FileSystem.AppDataDirectory;
        //string filename = "xyz.json";
        //var completeFilePath = Path.Combine(pathToAppDictionary, filename);
        //string contentToWrite = "Test: Information";
        //await File.WriteAllTextAsync(completeFilePath, contentToWrite);
    }

    private void SB_FilterCourses_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (BindingContext is VM_Courses vm)
        {
            CarV_Courses.ItemsSource = vm.GetFilteredItems(e.NewTextValue);
        }
    }

    //private void e_username_Unfocused(object sender, FocusEventArgs e)
    //{
    //    string _oldUsername = App._userData.Username;

    //}

    // Change Search bar Icon to white on Android
    private void changeSearchBarIconColor()
    {
        SearchBarHandler.Mapper.AppendToMapping("CustomSearchIconColor", (handler, view)=>
        {
#if ANDROID 
            var context = handler.PlatformView.Context;
            var searchIconId = context.Resources.GetIdentifier("search_mag_icon", "id", context.PackageName);
            if (searchIconId != 0)
            {
                var searchIcon = handler.PlatformView.FindViewById<Android.Widget.ImageView>(searchIconId);
                searchIcon.SetColorFilter(Android.Graphics.Color.White, Android.Graphics.PorterDuff.Mode.SrcIn);
            }
#endif
        });
    }
}