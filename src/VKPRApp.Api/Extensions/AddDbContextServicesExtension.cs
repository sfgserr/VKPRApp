using Microsoft.EntityFrameworkCore;
using VKPRApp.Data;

namespace VKPRApp.Api.Extensions
{
    static class AddDbContextServicesExtension
    {
        public static IServiceCollection AddDbContextServices(this IServiceCollection services, string connectionString)
        {
            Action<DbContextOptionsBuilder> optionsBuilder = o => o.UseSqlServer(connectionString);

            services.AddDbContext<VKPRAppDbContext>(optionsBuilder);
            services.AddSingleton<VKPRAppDbContextFactory>(s => new VKPRAppDbContextFactory(optionsBuilder));

            return services;
        }
    }
}
