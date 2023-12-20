using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using daprota.Models;
using daprota.Pages;
using daprota.Services;
using System.Collections.ObjectModel;
using static System.Net.Mime.MediaTypeNames;

namespace daprota.ViewModels
{
    public partial class VM_Intro : ObservableObject
    {
        [ObservableProperty]
        private int currentLessonId;

        [ObservableProperty]
        public M_CourseDetails courseDetails;

        [ObservableProperty]
        public M_CurrentCourse currentCourse;

        [ObservableProperty]
        public List<M_UserResponse> userResponses;

        [ObservableProperty]
        public List<M_ChatAnswer> chatAnswer;

        [ObservableProperty]
        public ObservableCollection<M_ChatAnswer> answer;

        [ObservableProperty]
        public M_Conversation conversation;

        [ObservableProperty]
        public ObservableCollection<M_ChatMsg> chat;

        [ObservableProperty]
        public List<M_BotMsg> botMsgs;

        [ObservableProperty]
        public bool isAnswerSelected;
        
        [ObservableProperty]
        public bool lessonDone = false;

        public M_User CurrentUser { get; set; }
        public bool lastAnswer;
        public bool isLesson2 = false;
        public bool explainationNeeded = false;

        public static int _msgId;

        private int correctAnswers;
        private int inCorrectAnswers;
        private int incorrectAnswerCount;
        private Data _data;


        public VM_Intro(Data d)
        {
            _data = d;
            _data.GetUser();
            CurrentLessonId = Data.SelectedLessonId;
            CurrentUser = Data.UserData;
            Answer = new();
            Chat = new ObservableCollection<M_ChatMsg>();
            VM_Intro._msgId = 0;
            IsAnswerSelected = false;
            lastAnswer = true;
            incorrectAnswerCount = 0;
        }

        public async Task LoadData()
        {
            IsLesson2(Data.SelectedLessonId);
            await _data.GetCurrentCourse();
            CurrentCourse = Data.CurrentCourse;
            CourseDetails = await _data.GenerateAsyncCourseDetails(CurrentCourse);
            Conversation = await _data.GenerateAsyncConversation(CurrentLessonId);

            await Task.Delay(500);
            NextBotMsg();
            await Task.Delay(500);
            GenerateResponses();
        }

        private void IsLesson2(int lessonId)
        {
            if (lessonId == 2) {
                isLesson2 = true;
            };
        }

        public void AddBotExplaination()
        {
            Chat.Add(new M_ChatMsg()
            {
                Name = "Bot",
                IsPos = false,
                Text = Conversation.BotMsgList[VM_Intro._msgId].Explaination,
                Image = "bot.png"
            });
            explainationNeeded = false;
        }

        public void NextBotMsg()
        {
            Chat.Add(new M_ChatMsg()
            {
                Name= "Bot",
                IsPos = false,
                Text = Conversation.BotMsgList[VM_Intro._msgId].Statement,
                Image="bot.png"
            });
        }

        // for Quiz -> IMPORTANT
        public void GenerateAnswers()
        {
     
            //SubsetOfAnswers = new();
            //List<M_Answer> answers = CourseDetails.Answers;

            //List<M_Answer> correctAnswers = answers.Where(a => a.IsCorrect).ToList();
            //List<M_Answer> incorrectAnswers = answers.Where(a => !a.IsCorrect).ToList();

            //Random rnd = new Random();
            //int randomIndex= rnd.Next(correctAnswers.Count);
            //SubsetOfAnswers.Add(correctAnswers[randomIndex]);

            //randomIndex = rnd.Next(incorrectAnswers.Count);
            //SubsetOfAnswers.Add(incorrectAnswers[randomIndex]);
            //randomIndex = rnd.Next(incorrectAnswers.Count);
            //SubsetOfAnswers.Add(incorrectAnswers[randomIndex]);
            //Random rng = new Random();
            //int n = SubsetOfAnswers.Count;
            //while (n > 1)
            //{
            //    n--;
            //    int k = rng.Next(n + 1);
            //    M_Answer value = SubsetOfAnswers[k];
            //    SubsetOfAnswers[k] = SubsetOfAnswers[n];
            //    SubsetOfAnswers[n] = value;
            //}
        }

        public void GenerateResponses()
        {
            Random rnd = new Random();
            int firstAnswer = rnd.Next(1, 3);
            List<M_ChatAnswer> responses = new();


            Answer.Clear();
            switch (firstAnswer)
            {
                case 1:
                    Answer.Add(new M_ChatAnswer()
                    {
                        Text = Conversation.UserResponseList[VM_Intro._msgId].NegativeResponse,
                        IsPos = false,
                    });
                    Answer.Add(new M_ChatAnswer()
                    {
                        Text = Conversation.UserResponseList[VM_Intro._msgId].PositiveResponse,
                        IsPos = true
                    });
                    break;
                case 2:
                    Answer.Add(new M_ChatAnswer()
                    {
                        Text = Conversation.UserResponseList[VM_Intro._msgId].PositiveResponse,
                        IsPos = true,
                    });
                    Answer.Add(new M_ChatAnswer()
                    {
                        Text = Conversation.UserResponseList[VM_Intro._msgId].NegativeResponse,
                        IsPos = false
                    });
                    break;
                default:
                    Answer.Add(new M_ChatAnswer()
                    {
                        Text = Conversation.UserResponseList[VM_Intro._msgId].NegativeResponse,
                        IsPos = false,
                    });
                    Answer.Add(new M_ChatAnswer()
                    {
                        Text = Conversation.UserResponseList[VM_Intro._msgId].PositiveResponse,
                        IsPos = true
                    });
                    break;
            }
    }

    [RelayCommand]
        public async Task AddChatMessage()
        {
            if (IsAnswerSelected)
            {   
                if (!lastAnswer && isLesson2)  
                {
                    incorrectAnswerCount++;
                    lastAnswer = true;
                }
                Answer.Clear();
                if (explainationNeeded)
                {
                    Chat.Add(new M_ChatMsg()
                    {
                        Name = Data.UserData.Username,
                        IsPos = false,
                        Text = Conversation.UserResponseList[VM_Intro._msgId].NegativeResponse,
                        Image = "user.png"
                    });
                    await Task.Delay(500);
                    AddBotExplaination();
                    await Task.Delay(500);
                }
                else
                {
                    Chat.Add(new M_ChatMsg()
                    {
                        Name = Data.UserData.Username,
                        IsPos = true,
                        Text = Conversation.UserResponseList[VM_Intro._msgId].PositiveResponse,
                        Image = "user.png"
                    });
                }
                if (VM_Intro._msgId < Conversation.BotMsgList.Count - 1)
                {
                    IncChatSquence();
                    await Task.Delay(500);
                    NextBotMsg();
                    GenerateResponses();
                } else
                {
                    LessonDone = true;
                }
            }
            IsAnswerSelected = false;
        }

        private void IncChatSquence()
        {
                VM_Intro._msgId++;
        }
        
        [RelayCommand]
        public async Task GoBack()
        {
            await Shell.Current.GoToAsync($"{nameof(CourseDetailsPage)}");

        }

        partial void OnLessonDoneChanged(bool oldValue, bool newValue)
        {
            if (isLesson2)
            {
                if (incorrectAnswerCount == 0)
                {
                    Chat.Add(new M_ChatMsg()
                    {
                        Name = "Bot",
                        IsPos = false,
                        Text = "Congratulations, you passed. You are ready for the Quiz!",
                        Image = "user.png"
                    });
                    // Save User Progress
                    if (Data.UserData.ActiveLessionId <= Data.SelectedLessonId)
                    {
                        int newLesson = Data.SelectedLessonId + 1;
                        Data.UserData.ActiveLessionId = newLesson;
                    }
                    _data.SetUserData(Data.UserData);
                } else
                {
                    int totalQuestions = 4;
                    int correctAnswers = totalQuestions - incorrectAnswerCount;
                    string msg = "Soory, you didn't pass this Lesson." + "You have answered " + correctAnswers + " out of " + totalQuestions + " Questions correctly." + "  Please try again to pass this Lesson.";
                    Chat.Add(new M_ChatMsg()
                    {
                        Name = "Bot",
                        IsPos = false,
                        Text = msg,
                        Image = "user.png"
                    });
                }
            } else {
                Chat.Add(new M_ChatMsg()
                {
                    Name = "Bot",
                    IsPos = false,
                    Text = "This is the end of this lesson. I have unlocked the next lesson for you. You can now exit this lesson using the arrow above and continue with the next lesson. I'm looking forward to next time!",
                    Image = "bot.png"
                });
                // Save User Progress
                if (Data.UserData.ActiveLessionId <= Data.SelectedLessonId)
                {
                    int newLesson = Data.SelectedLessonId + 1;
                    Data.UserData.ActiveLessionId = newLesson;
                }
                _data.SetUserData(Data.UserData);
            }
        }
    }
}
