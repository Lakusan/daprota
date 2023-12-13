using daprota.Models;
using daprota.Services;
using daprota.ViewModels;

namespace daprota.Pages;

public partial class CourseDetailsPage : ContentPage
{
	private VM_CourseDetails _vm;
    public M_User _user;
    private Data _data;

    public CourseDetailsPage(VM_CourseDetails vm, Data data)
	{
		InitializeComponent();
        BindingContext = vm;
		_vm = vm;
        _data = data;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.GetLessonData();
        _user = _data.GetUser();
    }

    private void ButtonCourseInfo_Clicked(object sender, EventArgs e)
    {
        if (!CourseInfoView.IsVisible)
        {
            CourseInfoView.IsVisible = true;
            LessonsView.IsVisible = false;

            if (App.Current.RequestedTheme == AppTheme.Light)
            {
                // #3498DB light blue
                // #F5F6FA white
                // #000000 black
                btn_CourseInfo.Background = Color.FromArgb("#3498DB");
                btn_CourseInfo.TextColor = Color.FromArgb("#F5F6FA");
                btn_Lesson.Background = Color.FromArgb("#F5F6FA");
                btn_Lesson.TextColor = Color.FromArgb("#000000");
            }
            else
            {
                // #2980B9 darker blue
                // #2C3E50 dark background blue
                btn_CourseInfo.Background = Color.FromArgb("#2980B9");
                btn_CourseInfo.TextColor = Color.FromArgb("#F5F6FA");
                btn_Lesson.Background = Color.FromArgb("#2C3E50");
            }
        }
    }

    private void ButtonLesson_Clicked(object sender, EventArgs e)
    {
        if (!LessonsView.IsVisible)
        {
            LessonsView.IsVisible = true;
            CourseInfoView.IsVisible = false;
            if (App.Current.RequestedTheme == AppTheme.Light)
            {
                // #3498DB light blue
                // #F5F6FA white
                // #000000 black
                btn_Lesson.Background = Color.FromArgb("#3498DB");
                btn_Lesson.TextColor = Color.FromArgb("#F5F6FA");
                btn_CourseInfo.Background = Color.FromArgb("#F5F6FA");
                btn_CourseInfo.TextColor = Color.FromArgb("#000000");
            }
            else
            {
                btn_Lesson.Background = Color.FromArgb("#2980B9");
                btn_Lesson.TextColor = Color.FromArgb("#F5F6FA");
                btn_CourseInfo.Background = Color.FromArgb("2C3E50");
            }
        }
    }
}