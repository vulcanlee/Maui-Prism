using PrismMonkey.Services;
using PrismMonkey.Views;

namespace PrismMonkey;

internal static class PrismStartup
{
    public static void Configure(PrismAppBuilder builder)
    {
        builder.RegisterTypes(RegisterTypes)
                .OnAppStart("NavigationPage/MainPage");
    }

    private static void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<MainPage>()
                     .RegisterInstance(SemanticScreenReader.Default);

        // 註冊 猴子服務
        containerRegistry.RegisterSingleton<MonkeyService>();
    }
}
