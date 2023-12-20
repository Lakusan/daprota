using daprota.Models;
using daprota.Services;
using daprota.ViewModels;

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

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _vm.IsAnswerSelected = true;
        var answer = (sender as CollectionView).SelectedItem as M_ChatAnswer;
        if (answer.IsPos)
        {
            _vm.explainationNeeded = false;
        }
        else
        {
            _vm.explainationNeeded = true;
            if(_vm.isLesson2)
            {
                _vm.lastAnswer = false;
            }
        }
    }
}