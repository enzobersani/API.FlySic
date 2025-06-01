using API.FlySic.Domain.Interfaces.Repositories.Base;
using API.FlySic.Domain.Notifications;
using Microsoft.EntityFrameworkCore;

namespace API.FlySic.Infrastructure.Repositories.Base
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly INotificationService _notifications;

        public BaseRepository(DbContext context, INotificationService notifications)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _notifications = notifications ?? throw new ArgumentNullException(nameof(notifications));
        }
        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
