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
    public M_CurrentCourse currentCourse { get; set; }

    public CoursesPage(VM_Courses vm)
    {
        InitializeComponent();
        BindingContext = vm;
        _vm = vm;
        //changeSearchBarIconColor();
        // maybe obsolet
        currentUser = _vm.GetCurrentUserProfile();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        currentUser = _vm.GetCurrentUserProfile();

        await _vm.LoadDataAsync();
        
        currentCourse = _vm.CurrentCourse;
        _vm.GetCourseProgressionFloat();
        _vm.GetCourseProgressionPercentage();
        _vm.SetCurrentCourseLessonProgress();


        CarV_Courses.ItemsSource = _vm.Courses;
    }

    private void SB_FilterCourses_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (BindingContext is VM_Courses vm)
        {
            CarV_Courses.ItemsSource = vm.GetFilteredItems(e.NewTextValue);
        }
    }

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

    private async void TapGestureRecognizer_MyCourseTapped(object sender, TappedEventArgs e)
    {
        await _vm.MyCourseTapped();
    }

    private async void TapGestureRecognizer_Settings_Tapped(object sender, TappedEventArgs e)
    {
        await _vm.SettingsTapped();
    }
}