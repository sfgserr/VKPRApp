using Microsoft.EntityFrameworkCore;

namespace VKPRApp.Data.Services
{
    public class NonQueryDataService<TDomainObject> where TDomainObject : Shared.Models.DomainObject
    {
        private readonly VKPRAppDbContextFactory _factory;

        public NonQueryDataService(VKPRAppDbContextFactory factory)
        {
            _factory = factory;
        }

        public async Task Create(TDomainObject entity)
        {
            using (VKPRAppDbContext context = _factory.Create())
            {
                try
                {
                    context.Set<TDomainObject>().Attach(entity);
                    context.Set<TDomainObject>().Add(entity);
                    await context.SaveChangesAsync();
                }
                catch
                {
                   
                }
            }
        }

        public async Task<TDomainObject> GetBy(Func<TDomainObject, bool> predicate)
        {
            using (VKPRAppDbContext context = _factory.Create())
            {
                List<TDomainObject> entities = await context.Set<TDomainObject>().ToListAsync();
                return entities.FirstOrDefault(predicate);
            }
        }

        public async Task<List<TDomainObject>> GetAll()
        {
            using (VKPRAppDbContext context = _factory.Create())
            {
                return await context.Set<TDomainObject>().ToListAsync();
            }
        }

        public async Task<TDomainObject> Update(TDomainObject entity)
        {
            using (VKPRAppDbContext context = _factory.Create())
            {   
                context.Set<TDomainObject>().Update(entity);
                await context.SaveChangesAsync();

                return entity;
            }
        }

        public async Task Remove(TDomainObject entity)
        {
            using (VKPRAppDbContext context = _factory.Create())
            {
                context.Set<TDomainObject>().Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}
