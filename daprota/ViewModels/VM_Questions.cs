using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using daprota.Models;
using daprota.Pages;
using daprota.Services;
using System.Collections.ObjectModel;

namespace daprota.ViewModels
{
    public partial class VM_Questions : ObservableObject
    {
        private Data _data;

        [ObservableProperty]
        public ObservableCollection<M_Question> questions;

        [ObservableProperty]
        ObservableCollection<M_QuizSequence> currentSequence;

        [ObservableProperty]
        List<M_QuizSequence> sequenceList;

        [ObservableProperty]
        ObservableCollection<M_Answer> currentAnswers;

        [ObservableProperty]
        public bool lessonDone = false;

        private int currentCourseId;
        private int currentLessonId;
        private const int MAXSEQUENCES = 6;

        public int sequenceCounter;
        public int quizScore;
        public bool IsAnswerSelected = false;
        public bool lastAnswer;
        private int incorrectAnswerCount;

        public VM_Questions(Data d)
        {
            _data = d;

            currentCourseId = Data.UserData.ActiveCourseId;
            currentLessonId = Data.UserData.ActiveLessionId;
            quizScore = 0;
            Questions = new ObservableCollection<M_Question>();
            SequenceList = new();
            sequenceCounter = 0;
            CurrentSequence = new();
            CurrentAnswers = new();
            incorrectAnswerCount = 0;
            LoadData();
        }

        public void LoadData()
        {
            GenerateQuestions();
            GenerateSquences();
            GetQuestion();
        }

        public async void GenerateQuestions()
        {
            List<M_Question> allQuestions = await _data.GetQuestions();
            List<M_Question> tmpList = allQuestions.FindAll(q => q.CourseId == currentCourseId);

            // find 6 Random Questions
            int numItems = MAXSEQUENCES;
            Random random = new Random();

            for (int i = 0; i < numItems; i++)
            {
                int randomIndex = random.Next(0, tmpList.Count - 1);
                M_Question randomQuestion = tmpList[randomIndex];
                Questions.Add(randomQuestion);
            }
        }

        public async void GenerateSquences()
        {

            List<M_Answer> allAnswers = await _data.GetAnswers();
            List<M_Answer> lessonAnswers = allAnswers.FindAll(q => q.CourseId == currentCourseId);


            List<M_Answer> correctAnswers = lessonAnswers.Where(a => a.IsCorrect).ToList();
            List<M_Answer> incorrectAnswers = lessonAnswers.Where(a => !a.IsCorrect).ToList();

            // find Answers to Questions
            int idCounter = 0;
            foreach (var item in Questions)
            {

                M_Answer correctAnswer = correctAnswers.First(a => a.QuestionId == item.Id && a.IsCorrect);
                M_Answer falseAnswer = incorrectAnswers.First(a => a.QuestionId == item.Id && !a.IsCorrect);
                if (correctAnswer != null && falseAnswer != null)
                {
                    SequenceList.Add(new M_QuizSequence()
                    {
                        Id = idCounter,
                        QuestionText = item.QuestionText,
                        CorrectAnswer = correctAnswer.AnswerText,
                        FalseAnswer = falseAnswer.AnswerText,
                    });

                    idCounter++;
                }
            }
        }

        public void GetQuestion()
        {
            if (sequenceCounter < MAXSEQUENCES)
            {
                Random rnd = new Random();
                int firstAnswer = rnd.Next(1, 3);
                CurrentSequence.Clear();
                CurrentSequence.Add(new M_QuizSequence()
                {
                    Id = this.sequenceCounter,
                    QuestionText = SequenceList[sequenceCounter].QuestionText,
                });
                CurrentAnswers.Clear();

                switch (firstAnswer)
                {
                    case 1:
                        CurrentAnswers.Add(new M_Answer()
                        {
                            Id = this.sequenceCounter,
                            AnswerId = this.sequenceCounter,
                            QuestionId = SequenceList[sequenceCounter].Id,
                            CourseId = this.sequenceCounter,
                            IsCorrect = true,
                            ExplainationText = "true",
                            AnswerText = SequenceList[sequenceCounter].CorrectAnswer,
                        });
                        CurrentAnswers.Add(new M_Answer()
                        {
                            Id = this.sequenceCounter,
                            AnswerId = this.sequenceCounter,
                            QuestionId = SequenceList[sequenceCounter].Id,
                            CourseId = this.sequenceCounter,
                            IsCorrect = false,
                            ExplainationText = "false",
                            AnswerText = SequenceList[sequenceCounter].FalseAnswer,
                        });
                        break;
                    case 2:
                        CurrentAnswers.Add(new M_Answer()
                        {
                            Id = this.sequenceCounter,
                            AnswerId = this.sequenceCounter,
                            QuestionId = SequenceList[sequenceCounter].Id,
                            CourseId = this.sequenceCounter,
                            IsCorrect = false,
                            ExplainationText = "false",
                            AnswerText = SequenceList[sequenceCounter].FalseAnswer,
                        });
                        CurrentAnswers.Add(new M_Answer()
                        {
                            Id = this.sequenceCounter,
                            AnswerId = this.sequenceCounter,
                            QuestionId = SequenceList[sequenceCounter].Id,
                            CourseId = this.sequenceCounter,
                            IsCorrect = true,
                            ExplainationText = "true",
                            AnswerText = SequenceList[sequenceCounter].CorrectAnswer,
                        });
                        break;
                    default:
                        CurrentAnswers.Add(new M_Answer()
                        {
                            Id = this.sequenceCounter,
                            AnswerId = this.sequenceCounter,
                            QuestionId = SequenceList[sequenceCounter].Id,
                            CourseId = this.sequenceCounter,
                            IsCorrect = true,
                            ExplainationText = "true",
                            AnswerText = SequenceList[sequenceCounter].CorrectAnswer,
                        });
                        CurrentAnswers.Add(new M_Answer()
                        {
                            Id = this.sequenceCounter,
                            AnswerId = this.sequenceCounter,
                            QuestionId = SequenceList[sequenceCounter].Id,
                            CourseId = this.sequenceCounter,
                            IsCorrect = false,
                            ExplainationText = "false",
                            AnswerText = SequenceList[sequenceCounter].FalseAnswer,
                        });
                        break;
                }
            }
        }

        [RelayCommand]
        public async Task GoBack()
        {
            await Shell.Current.GoToAsync($"{nameof(CourseDetailsPage)}");

        }

        [RelayCommand]
        public async Task AnswerSelected()
        {
            if (IsAnswerSelected)
            {
                if (sequenceCounter < MAXSEQUENCES)
                {
                    if (!lastAnswer)
                    {
                        incorrectAnswerCount++;
                        lastAnswer = true;
                    }
                    GetQuestion();
                } else 
                {
                    int correctAnserwersCount = MAXSEQUENCES - incorrectAnswerCount;
                    if (incorrectAnswerCount > 2)
                    {
                        Shell.Current.DisplayAlert("Sorry, you have not passed", $"You got {correctAnserwersCount} correct Answer(s). You need to have at least 4 correct ones out of 6.", "Try Again");
                        await Shell.Current.GoToAsync($"{nameof(CourseDetailsPage)}");
                    } else
                    {
                        Shell.Current.DisplayAlert("Congratulations, you have passed", $"You got {correctAnserwersCount} Answers correctly.", "Proceed to the next Course");
                        LessonDone = true;
                        await Shell.Current.GoToAsync($"{nameof(CourseDetailsPage)}");
                    }
                }
                sequenceCounter++;
            }
        }

        partial void OnLessonDoneChanged(bool oldValue, bool newValue)
        {
            // Save User Progress
            if (Data.UserData.ActiveLessionId <= Data.SelectedLessonId)
            {
                int newLesson = Data.SelectedLessonId + 1;
                Data.UserData.ActiveLessionId = newLesson;
                //Data.UserData.ActiveCourseId = Data.UserData.ActiveCourseId + 1;
            }
            _data.SetUserData(Data.UserData);
        }
    }
}
