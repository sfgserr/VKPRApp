using VKPRApp.ViewModels;

namespace VKPRApp.MauiAppBuilderExtensions
{
    static class AddViewModelsMauiAppBuilderExtension
    {
        public static MauiAppBuilder AddViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<InfoViewModel>();
            builder.Services.AddTransient<TasksViewModel>();
            builder.Services.AddTransient<NotificationsViewModel>();
            builder.Services.AddTransient<RepostPaymentViewModel>();
            builder.Services.AddTransient<GuideTaskViewModel>();
            builder.Services.AddTransient<SuccessfullCompletedTaskViewModel>();
            builder.Services.AddTransient<FailedTaskViewModel>();
            builder.Services.AddTransient<LinkCardViewModel>();
            builder.Services.AddTransient<WelcomeViewModel>();

            return builder;
        }
    }
}
