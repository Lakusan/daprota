using daprota.Models;
using daprota.ViewModels;

namespace daprota.Pages;

public partial class QuestionsPage : ContentPage
{
    public VM_Questions _vm;
    public QuestionsPage(VM_Questions vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
    private async void Back_Tapped(object sender, TappedEventArgs e)
    {
        await _vm.GoBack();
    }
    public void Answers_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _vm.IsAnswerSelected = true;
        var answer = (sender as CollectionView).SelectedItem as M_Answer;
        if (!answer.IsCorrect)
        {
            _vm.lastAnswer = false;
        } else
        {
            _vm.lastAnswer = true;
        }

    }
}