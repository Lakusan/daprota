using daprota.Models;
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
        public static M_User? _userData;
        public static M_UserProgess? _userProgess;

        public App()
        {
            InitializeComponent();


            // Get User Data 
            // TODO: get user data from xml.
            string tmp = Preferences.Default.Get<string>("Settings", null);
            if (tmp != null)
            {
                // assume that in tmp is JSON String
                _userData = JsonSerializer.Deserialize<M_User>(tmp);
            }
            else
            {
                // otherwise use defaults
                _userData = new M_User()
                {
                    UserId = 0,
                    Username = "Default",
                    LastActivityDate = DateOnly.FromDateTime(DateTime.Now),
                    CoursesCompleted = new List<int> { 0, 1, 2 }
                };
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
            MainPage = new AppShell();
        }
    }
}
