using daprota.Models;
using daprota.Services;
using System.ComponentModel;
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
        private Storage _storage;
        private Data _data;

        public App(Storage s, Data d)
        {
            InitializeComponent();
            _storage = s;
            _data = d;

            M_User defaultUser = new M_User()
            {
                UserId = 1,
                Username = "Username",
                FirstStart = true,
                ActiveCourseId = 0,
                ActiveLessionId = 0,

            };
            Data.DefaultUserProfile = defaultUser;
            // Check if User is in Prefs
            bool hasSettings = Preferences.Default.ContainsKey("Settings", null);
            if (!hasSettings)
            {
                _data.SetUserData(defaultUser);
                _data.GetUser();
            }
            else
            {
                _data.GetUser();
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
