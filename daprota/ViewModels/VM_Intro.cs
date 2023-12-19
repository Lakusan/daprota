using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using daprota.Models;
using daprota.Pages;
using daprota.Services;
using System.Collections.ObjectModel;

namespace daprota.ViewModels
{
    
    public partial class VM_Intro : ObservableObject
    {
        [ObservableProperty]
        public M_CourseDetails courseDetails;

        [ObservableProperty]
        public M_CurrentCourse currentCourse;
        private Data _data;

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
        
        public static int _msgId;
        public bool explainationNeeded = false;
        [ObservableProperty]
        public bool lessonDone = false;

        public M_User CurrentUser { get; set; }


        public VM_Intro(Data d)
        {
            _data = d;
            _data.GetUser();
            CurrentUser = Data.UserData;
            //CourseDetails = new();
            //CurrentCourse = new();
            //UserResponses = new();
            Answer = new();
            //ChatAnswer = new List<M_ChatAnswer>();
            //string greeting = "Hello " + CurrentUser.Username;
            //Chat = new ObservableCollection<M_ChatMsg>()
            //{
            //    new M_ChatMsg()
            //    {
            //        Name = "Bot",
            //        IsPos = false,
            //        Text = greeting,
            //        Image= "bot.png"
            //    }
            //};
            Chat = new ObservableCollection<M_ChatMsg>();
            VM_Intro._msgId = 0;
            IsAnswerSelected = false;
        }

    public async Task LoadData()
        {
            await _data.GetCurrentCourse();
            CurrentCourse = Data.CurrentCourse;
            CourseDetails = await _data.GenerateAsyncCourseDetails(CurrentCourse);
            Conversation = await _data.GenerateAsyncConversation(CurrentCourse.CurrentCurseId);

            await Task.Delay(500);
            NextBotMsg();
            await Task.Delay(500);
            GenerateResponses();
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
            Answer.Clear();
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
    }

    [RelayCommand]
        public async Task AddChatMessage()
        {
            if (IsAnswerSelected)
            {
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
            Chat.Add(new M_ChatMsg()
            {
                Name = "Bot",
                IsPos = false,
                Text = "This is the end of this lesson. I have unlocked the next lesson for you. You can now exit this lesson using the arrow above and continue with the next lesson. I'm looking forward to next time!",
                Image = "user.png"
            });
            // Save User Progress
            if (CurrentCourse.CurrentLessonId < CurrentCourse.CurrentLessonId)
            {
                int newLesson = CurrentCourse.CurrentLessonId + 1;
                CurrentUser.ActiveLessionId = newLesson;
            }               
                
            Chat.Add(new M_ChatMsg()
            {
                Name = "Bot",
                IsPos = false,
                Text = CurrentUser.ActiveLessionId.ToString(),
                Image = "user.png"
            });
            _data.SetUserData(CurrentUser);
        }
    }
}
