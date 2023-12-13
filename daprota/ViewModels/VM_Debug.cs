using CommunityToolkit.Mvvm.ComponentModel;
using daprota.Models;
using daprota.Services;

namespace daprota.ViewModels
{
    public partial class VM_Debug : ObservableObject
    {
        [ObservableProperty]
        public List<M_Course> course;

        public VM_Debug()
        {
            Course = Data.Courses;
        }
        public async Task LoadAsyncCourses()
        {

        }
    }
}
