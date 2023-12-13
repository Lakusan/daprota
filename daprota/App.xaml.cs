using daprota.Models;
using daprota.Services;
using System.Text.Json;

#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif


namespace daprota
{
    public partial class App : Application
    {
        public static M_User _userData;

        public static M_User _defaulUserProfile;
        
        // CONSTANTS -> Maybe obsolet
        public static int MAX_LESSONS_PER_COURSE = 4;

        public App()
        {
            InitializeComponent();
            // Set default User Profile
            _defaulUserProfile = new M_User()
            {
                UserId = 0,
                Username = "Username",
                LastActivityDate = DateOnly.FromDateTime(DateTime.Now),
                FirstStart = true,
                ActiveCourseId = 0,
                ActiveLessionId = 0,
            };

            // Get User Data from Prefs on app start
            string tmp = Preferences.Default.Get<string>("Settings", null);
            // if key is !null in prefs, load User Profile in M_User
            if (tmp != null)
            {
                _userData = JsonSerializer.Deserialize<M_User>(tmp);
            }
            else
            {
                // if key is null
                // create default User Profile as M_User for runtime
                _userData = _defaulUserProfile;
                // Set default user profile in Prefs with key "Settings"
                var jsonString = JsonSerializer.Serialize(_userData);
                Preferences.Default.Set("Settings", jsonString);
            }

            Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>
            {
#if WINDOWS
                var mauiWindow = handler.VirtualView;
                var nativeWindow = handler.PlatformView;
                nativeWindow.Activate();
                IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
                WindowId windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle);
                AppWindow appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
                appWindow.Resize(new SizeInt32(450, 850));
#endif  
            });
            // initialized courses
            MainPage = new AppShell();
        }
    }
}
