namespace AttemptsGO;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<LobbyViewModel>();

		builder.Services.AddSingleton<LobbyPage>();

        builder.Services.AddSingleton<GameViewModel>();

		builder.Services.AddSingleton<GamePage>();

		builder.Services.AddSingleton<HistoryPopup>();

		Routing.RegisterRoute("LobbyPage", typeof(LobbyPage));
		Routing.RegisterRoute("GamePage", typeof(GamePage));
		Routing.RegisterRoute("HistoryPage", typeof(HistoryPopup));
        return builder.Build();
	}
}
