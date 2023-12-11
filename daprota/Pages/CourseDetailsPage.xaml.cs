using daprota.ViewModels;

namespace daprota.Pages;

public partial class CourseDetailsPage : ContentPage
{
	private VM_CourseDetails _vm;
	public CourseDetailsPage(VM_CourseDetails vm)
	{
		InitializeComponent();
		BindingContext = vm;
		_vm = vm;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

    }

}