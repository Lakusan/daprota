using daprota.ViewModels;

namespace daprota.Pages;

public partial class CourseDetailsPage : ContentPage
{
	public CourseDetailsPage(VM_CourseDetails vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}