using Microsoft.EntityFrameworkCore;
using VKPRApp.Shared.Services;

namespace VKPRApp.Data.Services
{
    public class UserDataService : IUserService
    {
        private readonly NonQueryDataService<Shared.Models.User> _dataService;
        private readonly VKPRAppDbContextFactory _factory;

        public UserDataService(NonQueryDataService<Shared.Models.User> dataService, VKPRAppDbContextFactory factory)
        {
            _dataService = dataService;
            _factory = factory;
        }

        public async Task AddUser(Shared.Models.User user)
        {
            await _dataService.Create(user);
        }

        public async Task<Shared.Models.User> GetUserByUserId(string userId)
        {
            using (VKPRAppDbContext context = _factory.Create())
            {
                return await context.Users
                                    .Include(u => u.Tasks)
                                    .Include(u => u.VKUser)
                                    .Include(u => u.BankCard)
                                    .Include(u => u.Subscription)
                                    .FirstOrDefaultAsync(u => u.VKUser.UserId == userId);
            }
        }

        public async Task<List<Shared.Models.User>> GetUsers()
        {
            using (VKPRAppDbContext context = _factory.Create())
            {
                return await context.Users
                                    .Include(u => u.Tasks)
                                    .Include(u => u.VKUser)
                                    .Include(u => u.BankCard)
                                    .Include(u => u.Subscription)
                                    .ToListAsync();

            }
        }

        public async Task<Shared.Models.User> UpdateUser(Shared.Models.User user)
        {
            return await _dataService.Update(user);
        }

        public async Task<Shared.Models.User> UpdateAutoRenewal(string userId, bool value)
        {
            using (VKPRAppDbContext context = _factory.Create())
            {
                Shared.Models.User user = await GetUserByUserId(userId);
                context.Attach(user);

                user.Subscription.AutoRenewal = value;

                await context.SaveChangesAsync();

                return user;
            }
        }

        public async Task<Shared.Models.User> CompleteTask(Shared.Models.TaskType taskType, string userId)
        {
            using (VKPRAppDbContext context = _factory.Create())
            {
                Shared.Models.User user = await GetUserByUserId(userId);
                context.Attach(user);

                Shared.Models.Task taskToRemove = user.Tasks.FirstOrDefault(t => t.TaskType == taskType);
                user.Tasks.Remove(taskToRemove);

                await context.SaveChangesAsync();

                return user;
            }
        }

        public async Task<Shared.Models.User> SetBankCardToNull(string userId)
        {
            using (VKPRAppDbContext context = _factory.Create())
            {
                Shared.Models.User user = await GetUserByUserId(userId);
                context.Attach(user);

                user.BankCard = null;

                await context.SaveChangesAsync();

                return user;
            }
        }
    }
}
