using daprota.Services;
using daprota.ViewModels;
using System.Collections.ObjectModel;

namespace daprota.Pages;

public partial class IntroPage : ContentPage
{
    public VM_Intro _vm;
    public Data _data;

    public IntroPage(VM_Intro vm)
	{
		InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.LoadData();
    }

    private async void Back_Tapped(object sender, TappedEventArgs e)
    {
        await _vm.GoBack();
    }

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
    }

}