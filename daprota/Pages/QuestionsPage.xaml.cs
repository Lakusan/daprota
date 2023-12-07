using daprota.Models;

namespace daprota.Pages;

public partial class QuestionsPage : ContentPage
{
    public QuestionsPage()
    {
        InitializeComponent();
    }

    private void btn_generateQuestion_Clicked(object sender, EventArgs e)
    {
        M_Question question = new M_Question()
        {
            qText = "Some Explainaton to the question"
        };
        question.qAnswers = new List<M_Answer>
        {
            new M_Answer() {Text="Answertext1", isCorrect=true },
            new M_Answer() {Text="Answertext2", isCorrect=false },
            new M_Answer() {Text="Answertext3", isCorrect=false },
            new M_Answer() {Text="Answertext4", isCorrect=false }
        };
        
        question.qAnswers = question.qAnswers.OrderBy  (a => Guid.NewGuid()).ToList();
        VSL_Question.BindingContext = question;
    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var answer = (sender as CollectionView).SelectedItem as M_Answer;
        if (answer.isCorrect)
        {
            await DisplayAlert("Correct Answer", "Good Job - Explaination", "Proceed");
        }
        else
        {
            await DisplayAlert("Wrong Answer", "Explaination", "Try again");
        }
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        //M_User _userData = new M_User();

        ////var userPrefs = Preferences.Default.Get<string>("Settings", null);
        //if (App._userData != null)
        //{
        //    _userData = App._userData;
        //} else
        //{
        //    L_Test.Text = "ERROR";
        //}
        //L_Test.Text = _userData.Username;

        //if (userPrefs == null)
        //{
        //    _userData = JsonSerializer.Deserialize<M_User>(userPrefs);
        //    L_Test.Text = _userData.UserId.ToString();
        //}
        //else
        //{
        //    L_Test.Text = _userData.Username;
        //}
    }
}