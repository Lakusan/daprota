using daprota.Models;

namespace daprota.Services
{
    public class Data
    {
        public static List<M_CourseDetails> CourseDetails { get; set; }
        public static List<M_Course> Courses { get; set; }
        public static M_CurrentCourse CurrentCourse { get; set; }
        public static List<M_Question> Questions { get; set; }
        public static List<M_Answer> Answers { get; set; }
        public static List<M_Lesson> Lessons { get; set; }
        public static M_User DefaultUserProfile { get; set; }
        public static M_User UserData { get; set; }
        public static M_Conversation Conversation { get; set; }
        private static List<M_Conversation> ConversationList { get; set; }
        private static List<M_BotMsg> BotMsgList { get; set; }
        private static List<M_UserResponse> UserResponseList { get; set; }
        private static Storage _storage { get; set; }
        public static int SelectedLessonId {  get; set; }
        public static M_Course LastCourse { get; set; }
        public Data(Storage s)
        {
            _storage = s;
            SelectedLessonId = 0;
            Data.LastCourse = new();
            //Data.CourseDetails = new List<M_CourseDetails>();
            //Data.Courses = new List<M_Course>();
            //Data.Questions = new List<M_Question>();
            //Data.Answers = new List<M_Answer>();
            //Data.Lessons = new List<M_Lesson>();
            //Data.DefaultUserProfile = new M_User();
            //Data.UserData = new M_User();
            //Data.CurrentCourse = new M_CurrentCourse();

        }
        // Getters
        public M_User GetUser()
        {
                if (Data.UserData != null)
                {
                    return Data.UserData;
                } else
                {
                    Data.UserData = _storage.ConvertJSONStringToObj<M_User>(_storage.GetUserDataFromPrefs());
                    return Data.UserData;
                }
        }
        public async Task<List<M_Course>> GetCourses()
        {
            if (Data.Courses != null)
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
            if (Data.Questions != null)
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
            if (Data.Answers != null)
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
            if (Data.Lessons != null)
            {
                return Data.Lessons;
            }
            else
            {
                await LoadAsyncLessons();
                return Data.Lessons;
            }
        }
        public async Task<M_User> GetDefaultUserProfile()
        {
            if (Data.DefaultUserProfile != null)
            {
                return Data.DefaultUserProfile;
            }
            else
            {
                await LoadAsyncDefaultUserProfile();
                return Data.DefaultUserProfile;
            }
        }
        public int GetCourseProgressionPercentage()
        {
            int value;
            Data.UserData = GetUser();
            switch (Data.UserData.ActiveLessionId)
            {
                case 0:
                    value = 0;
                    break;
                case 1:
                    value = 25;
                    break;
                case 2:
                    value = 50;
                    break;
                case 3:
                    value = 75;
                    break;
                case 5:
                    value = 100;
                    break;
                default:
                    value = 0;
                    break;
            }
            return value;
        }
        public float GetCourseProgressionFloat()
        {
            float value = 0f;
            Data.UserData = GetUser();
            switch (Data.UserData.ActiveLessionId)
            {
                case 0:
                    value = 0.0f;
                    break;
                case 1:
                    value = 0.25f;
                    break;
                case 2:
                    value = .5f;
                    break;
                case 3:
                    value = .75f;
                    break;
                case 5:
                    value = 1f;
                    break;
                default:
                    value = 0f;
                    break;
            }
            return value;
        }
        public int GetCurrentLesson(int courseId)
        {
            Data.UserData = GetUser();
            return Data.UserData.ActiveLessionId;
        }
        public async Task GetCurrentCourse()
        {
            try
            {
                Data.CurrentCourse = new M_CurrentCourse()
                {
                    CurrentCurseId = Data.UserData.ActiveCourseId,
                    CurrentLessonId = Data.UserData.ActiveLessionId,
                    CurrentCourseTitle = GetCourseTitleFromCourseId(Data.UserData.ActiveCourseId),
                    Image = GetCourseImageFromCourseId(Data.UserData.ActiveCourseId),
                    Category = GetCourseCategoryFromCourseId(Data.UserData.ActiveCourseId),
                    Title = GetCourseTitleFromCourseId(Data.UserData.ActiveCourseId)
                };
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("ERROR ", $"DATA: {CurrentCourse}\n {ex}", "ok");
            }
        }
        public string GetCourseTitleFromCourseId(int courseId)
        {
            string? s = string.Empty;

            s = Courses.Where(course => course.Id == courseId)
                                  .Select(course => course.Title)
                                   .FirstOrDefault();
            if (s != null)
            {
                return s;
            }
            return "Error: not found";

        }
        public string GetCourseImageFromCourseId(int courseId)
        {
            string? s = Courses.Where(course => course.Id == courseId)
                               .Select(course => course.Image)
                               .FirstOrDefault();
            if (s != null)
            {
                return s;
            }
            return "Error: not found";
        }
        public string GetCourseCategoryFromCourseId(int courseId)
        {
            string? s = Courses.Where(course => course.Id == courseId)
                               .Select(course => course.Category)
                               .FirstOrDefault();
            if (s != null)
            {
                return s;
            }
            return "Error: not found";
        }
        public M_Course GetCourseById(int courseId)
        {
            M_Course? course = Data.Courses.Where(c => c.Id == courseId)
                         .FirstOrDefault();
            return course;
        }
        // Setters
        public void SetUserData(M_User newUser)
        {
            _storage.SetUserDataToPrefs(newUser);
        }
        public M_User SetUserName(string userName)
        {
            Data.UserData.Username = userName;
            _storage.SetUserDataToPrefs(Data.UserData);
            return Data.UserData;
        }
        // Loaders
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
            Data.DefaultUserProfile = await _storage.ReadEmbeddedXML<M_User>("defaultUserProfile.xml");
            Console.WriteLine(Data.DefaultUserProfile.ToString());
        }
        // Generators
        public async Task<M_CourseDetails> GenerateAsyncCourseDetails(M_CurrentCourse currentCourse)
        {
            // TODO: add User progress to lessons and calc course progresson based on passed lessons.
            // Add isDone to course as well
            GetUser();
            Data.Courses = await GetCourses();
            Data.Questions = await GetQuestions();
            Data.Answers = await GetAnswers();
            Data.Lessons = await GetLessons();
            Lessons = Data.Lessons;


            M_Course foundCourse = GetCourseById(currentCourse.CurrentCurseId);

            M_CourseDetails newCourseDetails = new M_CourseDetails()
            {
                Course = foundCourse,
                Lessons = Lessons.FindAll(l => l.CourseId == currentCourse.CurrentCurseId),
                Questions = Questions.FindAll(q => q.LessonId == currentCourse.CurrentCurseId),
                Answers = Answers.FindAll(a => a.Id == currentCourse.CurrentCurseId),
            };
            return newCourseDetails;
        }
        public async Task<M_Conversation> GenerateAsyncConversation(int currentCourseId)
        {
            ConversationList = await _storage.ReadEmbeddedXML<List<M_Conversation>>("conversations.xml");
            BotMsgList = await _storage.ReadEmbeddedXML<List<M_BotMsg>>("botmsg.xml");
            UserResponseList = await _storage.ReadEmbeddedXML<List<M_UserResponse>>("userresponses.xml");

            M_Conversation foundConversation = ConversationList.Find(c => c.ConversationId == currentCourseId);
            List<M_BotMsg> foundBotMsgs = BotMsgList.FindAll(b => b.ConversationId == currentCourseId);
            List<M_UserResponse> foundUserResponses = UserResponseList.FindAll(u => u.ConversationId == currentCourseId);

            foundConversation.BotMsgList.Clear();
            foundConversation.BotMsgList.AddRange(foundBotMsgs);

            foundConversation.UserResponseList.Clear();
            foundConversation.UserResponseList.AddRange(foundUserResponses);
            return foundConversation;
        }
    }
}
