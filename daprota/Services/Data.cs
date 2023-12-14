using daprota.Models;

namespace daprota.Services
{
    public class Data
    {
        public static List<M_CourseDetails> CourseDetails { get; set; }
        public static List<M_Course> Courses { get; set; }
        public static List<M_Question> Questions { get; set; }
        public static List<M_Answer> Answers { get; set; }
        public static List<M_Lesson> Lessons { get; set; }
        public static List<M_User> DefaultUserProfile { get; set; }
        public static M_User UserData { get; set; }
        private static Storage _storage { get; set; }

        public Data(Storage s)
        {
            _storage = s;
            Data.CourseDetails = new List<M_CourseDetails>();
            Data.Courses = new List<M_Course>();
            Data.Questions = new List<M_Question>();
            Data.Answers = new List<M_Answer>();
            Data.Lessons = new List<M_Lesson>();
            Data.DefaultUserProfile = new List<M_User>();

        }

        public M_User GetUser()
        {
            if(Data.UserData != null)
            {
                return Data.UserData;
            }
            else
            {
                UserData = _storage.ConvertJSONStringToObj<M_User>(_storage.GetUserDataFromPrefs());
                return Data.UserData;
            }
                
        }
        public async Task<List<M_Course>> GetCourses()
        {
            if (Data.Courses.Count != 0)
            {
                return Data.Courses;
            }
            else
            {
                await LoadAsyncCourses();
                return Data.Courses;
            }
        }
        public async Task<List<M_Question>> GetQuestions()
        {
            if (Data.Questions.Count != 0)
            {
                return Data.Questions;
            }
            else
            {
                await LoadAsyncQuestions();
                return Data.Questions;
            }
        }
        public async Task<List<M_Answer>> GetAnswers()
        {
            if (Data.Answers.Count != 0)
            {
                return Data.Answers;
            }
            else
            {
                await LoadAsyncAnswers();
                return Data.Answers;
            }
        }
        public async Task<List<M_Lesson>> GetLessons()
        {
            if (Data.Lessons.Count != 0)
            {
                return Data.Lessons;
            }
            else
            {
                await LoadAsyncLessons();
                return Data.Lessons;
            }
        }
        public async Task<List<M_User>> GetDefaultUserProfile()
        {
            if (Data.DefaultUserProfile.Count != 0)
            {
                return Data.DefaultUserProfile;
            }
            else
            {
                await LoadAsyncDefaultUserProfile();
                return Data.DefaultUserProfile;
            }
        }
        public async Task LoadAsyncQuestions()
        {
            Data.Questions = await _storage.ReadEmbeddedXML<List<M_Question>>("questions.xml");
        }
        public async Task LoadAsyncCourses()
        {
            Data.Courses = await _storage.ReadEmbeddedXML<List<M_Course>>("courses.xml");
        }
        public async Task LoadAsyncAnswers()
        {
            Data.Answers = await _storage.ReadEmbeddedXML<List<M_Answer>>("answers.xml");
        }
        public async Task LoadAsyncLessons()
        {
            Data.Lessons = await _storage.ReadEmbeddedXML<List<M_Lesson>>("lessons.xml");
        }
        public async Task LoadAsyncDefaultUserProfile()
        {
            Data.DefaultUserProfile = await _storage.ReadEmbeddedXML<List<M_User>>("defaultUserProfile.xml");
        }
        public async Task<M_CourseDetails> GenerateAsyncCourseDetails(M_Course course)
        {
            // TODO: add User progress to lessons and calc course progresson based on passed lessons.
            // Add isDone to course as well
            Data.Courses = await GetCourses();
            Data.Questions = await GetQuestions();
            Data.Answers = await GetAnswers();
            Data.Lessons = await GetLessons();

            M_CourseDetails newCourseDetails = new M_CourseDetails()
            {
                Course = course,
                Lessons = Lessons.FindAll(l =>  l.CourseId == course.Id),
                Questions = Questions.FindAll(q => q.LessonId == course.Id),
                Answers = Answers.FindAll(a => a.Id == course.Id),
            };
            return newCourseDetails;
        }
    }
}
