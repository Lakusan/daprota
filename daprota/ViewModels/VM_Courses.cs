using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using daprota.Models;
using daprota.Pages;
using daprota.Services;

namespace daprota.ViewModels
{
    public partial class VM_Courses : ObservableObject
    {
        public List<M_Course> Courses { get; set; }
        private Storage _storage;

        public VM_Courses(Storage s)
        {
            _storage = s;
            //Courses = new(){
            //    new M_Course
            //    {
            //        Id = 0,
            //        Title = "First Course",
            //        Description_long = "long course description",
            //        Description_short = "short course description",
            //        Image = "dotnet_bot.png"
            //    },
            //    new M_Course
            //    {
            //        Id = 1,
            //        Title = "Second Course",
            //        Description_long = "long course description",
            //        Description_short = "short course description",
            //        Image = "dotnet_bot.png"
            //    },
            //    new M_Course
            //    {
            //        Id = 2,
            //        Title = "Third Course",
            //        Description_long = "long course description",
            //        Description_short = "short course description",
            //        Image = "dotnet_bot.png"
            //    }
            //};
        }

        public async Task LoadDataAsync()
        {
            Courses = await _storage.ReadEmbeddedXML<List<M_Course>>("courses.xml"); 
        }
        [RelayCommand]
        async Task Tap(int id)
        {
            await Shell.Current.GoToAsync($"{nameof(CourseDetailsPage)}?Id={id}");
        }
    }
}
