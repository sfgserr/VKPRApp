using VKPRApp.Views;

namespace VKPRApp.MauiAppBuilderExtensions
{
    static class AddPagesMauiAppBuilderExtension
    {
        public static MauiAppBuilder AddPages(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<TasksPage>();
            builder.Services.AddTransient<NotificationsPage>();
            builder.Services.AddTransient<InfoPage>();
            builder.Services.AddTransient<RepostPaymentPage>();
            builder.Services.AddTransient<GuideTaskPage>();
            builder.Services.AddTransient<SuccessfullCompletedTaskPage>();
            builder.Services.AddTransient<FailedTaskPage>();
            builder.Services.AddTransient<LinkCardPage>();
            builder.Services.AddTransient<WelcomePage>();

            return builder;
        }
    }
}
