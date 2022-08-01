using Foundation;

namespace PrismMonkey;

[Register(nameof(AppDelegate))]
public class AppDelegate : MauiUIApplicationDelegate
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
