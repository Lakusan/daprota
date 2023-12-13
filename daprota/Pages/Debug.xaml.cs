using daprota.ViewModels;

namespace daprota.Pages;

public partial class Debug : ContentPage
{
	private VM_Debug _vm;
	public Debug(VM_Debug vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}