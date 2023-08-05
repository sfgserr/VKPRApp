using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using VKPRApp.Data.Services;
using VKPRApp.Shared.Services;
using VKPRApp.Api.Extensions;

namespace VKPRApp.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(o =>
            {
                o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddOAuth("VK", "VKontakte", config =>
            {
                config.ClientId = _configuration["Authentication:VK:Id"];
                config.ClientSecret = _configuration["Authentication:VK:Secret"];
                config.ClaimsIssuer = "VKontakte";
                config.CallbackPath = new PathString("/signin-vkontakte-token");
                config.AuthorizationEndpoint = "https://oauth.vk.com/authorize";
                config.TokenEndpoint = "https://oauth.vk.com/access_token";
                config.Scope.Add("wall");
                config.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "user_id");
                config.SaveTokens = true;
                config.Events = new OAuthEvents
                {
                    OnCreatingTicket = context =>
                    {
                        context.RunClaimActions(context.TokenResponse.Response.RootElement);
                        return Task.CompletedTask;
                    },
                    OnRemoteFailure = OnFailure
                };
            });

            services.AddAuthorization();
            services.AddRazorPages();
            services.AddSingleton<NonQueryDataService<Shared.Models.User>>();
            services.AddDbContextServices(_configuration["ConnectionString"]);
            services.AddSingleton<IUserService, UserDataService>();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }

        private Task OnFailure(RemoteFailureContext arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }
    }
}
