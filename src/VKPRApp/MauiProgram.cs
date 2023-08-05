using VKPRApp.MauiAppBuilderExtensions;

namespace VKPRApp;

public static class Constants
{
	public const string BaseUri = "https://sfgserr.bsite.net/";
	public const string CallBackScheme = "vkprapp";
}

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.AddServices()
			.AddViewModels()
			.AddPages()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("Proxima Nova Font.otf", "ProximaNova");
				fonts.AddFont("DancingScript-Regular.ttf", "Dancing");
			});

		return builder.Build();
	}
}
