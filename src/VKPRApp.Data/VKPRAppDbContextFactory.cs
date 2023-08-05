using Microsoft.EntityFrameworkCore;

namespace VKPRApp.Data
{
    public class VKPRAppDbContextFactory
    {
        private readonly Action<DbContextOptionsBuilder> _configureDb;

        public VKPRAppDbContextFactory(Action<DbContextOptionsBuilder> configureDb)
        {
            _configureDb = configureDb;
        }

        public VKPRAppDbContext Create()
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();

            _configureDb(optionsBuilder);

            return new VKPRAppDbContext(optionsBuilder.Options);
        }
    }
}
