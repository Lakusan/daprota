using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using daprota.Models;
using daprota.Pages;
using daprota.Services;
using System.Linq;

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
            // Send Dict of filtered Courses, if not filtered use _courses
            await Shell.Current.GoToAsync($"{nameof(CourseDetailsPage)}?Id={id}");
        }

        public List<M_Course> GetFilteredItems(string title)
        {
            return Courses.Where(course => course.Title.Contains(title)).ToList();
        }

        //[RelayCommand]
        //public  Task filterCourses(string filter)
        //{
        //    // filter Courses on user input 
        //    // restore if null or out of focus
        //    M_Course newItems = new();
        //    await newItems = Courses.Where(Course=> Course.Title.Contains(filter)).ToList();
        //    //List<int> listOfCourseId

        //}

    }
}
