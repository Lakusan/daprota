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

        // example use app Dictionary I/O
        var pathToAppDictionary = FileSystem.AppDataDirectory;
        string filename = "xyz.json";
        var completeFilePath = Path.Combine(pathToAppDictionary, filename);
        string contentToWrite = "Test: Information";
        await File.WriteAllTextAsync(completeFilePath, contentToWrite);
    }

    private void SB_FilterCourses_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (BindingContext is VM_Courses vm)
        {
            CarV_Courses.ItemsSource = vm.GetFilteredItems(e.NewTextValue);
            l_placeholder.Text = e.NewTextValue;
        }
    }
}