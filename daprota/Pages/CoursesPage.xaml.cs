using daprota.ViewModels;
using daprota.Models;

namespace daprota.Pages;

public partial class CoursesPage : ContentPage
{
    public List<M_Course> Courses { get; set; }
    private VM_Courses _vm;
    public CoursesPage(VM_Courses vm)
    {
        InitializeComponent();
        BindingContext = vm;
        _vm = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.LoadDataAsync();
        CarV_Courses.ItemsSource = _vm.Courses;
    }
}