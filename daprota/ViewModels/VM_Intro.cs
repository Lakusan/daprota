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
        public List<M_Answer> subsetOfAnswers;

        [ObservableProperty]
        public M_Conversation conversation;

        [ObservableProperty]
        public ObservableCollection<string> chat;

        public VM_Intro(Data d)
        {
            _data = d;
            CourseDetails = new();
            CurrentCourse = new();
            Chat = new() { new string("Hello") };
        }

        public async Task LoadData()
        {
            await _data.GetCurrentCourse();
            CurrentCourse = Data.CurrentCourse;
            //CourseDetails = await _data.GenerateAsyncCourseDetails(CurrentCourse);
            GenerateAnswers();

            Conversation = await _data.GenerateAsyncConversation(CurrentCourse.CurrentCurseId);
        }

        // for Quiz ->IMPORTANT
        public void GenerateAnswers()
        {
     
            SubsetOfAnswers = new();
            List<M_Answer> answers = CourseDetails.Answers;

            List<M_Answer> correctAnswers = answers.Where(a => a.IsCorrect).ToList();
            List<M_Answer> incorrectAnswers = answers.Where(a => !a.IsCorrect).ToList();

            Random rnd = new Random();
            int randomIndex= rnd.Next(correctAnswers.Count);
            SubsetOfAnswers.Add(correctAnswers[randomIndex]);

            randomIndex = rnd.Next(incorrectAnswers.Count);
            SubsetOfAnswers.Add(incorrectAnswers[randomIndex]);
            randomIndex = rnd.Next(incorrectAnswers.Count);
            SubsetOfAnswers.Add(incorrectAnswers[randomIndex]);
            Random rng = new Random();
            int n = SubsetOfAnswers.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                M_Answer value = SubsetOfAnswers[k];
                SubsetOfAnswers[k] = SubsetOfAnswers[n];
                SubsetOfAnswers[n] = value;
            }
        }
        [RelayCommand]
        public async Task AddChatMessage()
        {  
            Chat.Add(new string("Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message Message"));
        }

        [RelayCommand]
        public async Task GoBack()
        {
            await Shell.Current.GoToAsync($"{nameof(CourseDetailsPage)}");

        }
    }
}
