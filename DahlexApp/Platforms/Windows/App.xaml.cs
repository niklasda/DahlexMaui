using Microsoft.UI;
using Microsoft.UI.Xaml;
using Windows.Graphics;
using Microsoft.UI.Windowing;
using WinRT.Interop;
using System.Diagnostics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DahlexApp.WinUI;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : MauiWinUIApplication
{
	/// <summary>
	/// Initializes the singleton application object.  This is the first line of authored code
	/// executed, and as such is the logical equivalent of main() or WinMain().
	/// </summary>
	public App()
	{
		this.InitializeComponent();

        UnhandledException += (sender, e) =>
        {
            Debug.WriteLine(e.Exception.ToString());

            if (Debugger.IsAttached)
                Debugger.Break();


          //  e.Handled = true;
        };

    }

	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        base.OnLaunched(args);

        
        var currentWindow = Application.Windows.First().Handler!.PlatformView;
        IntPtr _windowHandle = WindowNative.GetWindowHandle(currentWindow);
        var windowId = Win32Interop.GetWindowIdFromWindow(_windowHandle);

        AppWindow appWindow = AppWindow.GetFromWindowId(windowId);
        appWindow.Resize(new SizeInt32(600, 1080));
    }
}
