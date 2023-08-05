using VKPRApp.Builders.ApiRequestBuilders;
using VKPRApp.Services;
using VKPRApp.Services.CheckTaskServices;
using VKPRApp.Services.RequestCreationServices;
using VKPRApp.Services.ResponseServices;
using VKPRApp.Services.UserActionServices;
using VKPRApp.Services.UserCreationServices;
using VKPRApp.Services.VkUrlServices;
using VKPRApp.Shared.Models;
using VKPRApp.Shared.Services;
using VKPRApp.Stores.Authenticators;
using VKPRApp.Stores.Installers;

namespace VKPRApp.MauiAppBuilderExtensions
{
    static class AddServicesMauiAppBuilderExtension
    {
        public static MauiAppBuilder AddServices(this MauiAppBuilder builder)
        { 
            builder.Services.AddSingleton<IAuthenticator, Authenticator>();
            builder.Services.AddSingleton<IVkUrlService, VkUrlService>();
            builder.Services.AddSingleton<IUserService, UserService>();
            builder.Services.AddSingleton<IApiRequestBuilder, ApiRequestBuilder>();
            builder.Services.AddSingleton<IUserCreationService, UserCreationService>();
            builder.Services.AddSingleton<ICheckTaskService, CheckTaskService>();
            builder.Services.AddSingleton<IResponseService, ResponseService>();
            builder.Services.AddSingleton<IInstaller, Installer>();
            builder.Services.AddSingleton<IRequestCreationService, RequestCreationService>();
            builder.Services.AddSingleton<IUserActionService, UserActionService>();

            builder.Services.AddSingleton<HttpClient>(s => new HttpClient());
            
            return builder;
        }
    }
}
