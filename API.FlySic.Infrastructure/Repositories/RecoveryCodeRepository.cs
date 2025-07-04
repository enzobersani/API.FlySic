using API.FlySic.Domain.Entities;
using API.FlySic.Domain.Interfaces.Repositories;
using API.FlySic.Domain.Notifications;
using API.FlySic.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FlySic.Infrastructure.Repositories
{
    public class RecoveryCodeRepository : BaseRepository<RecoveryCode>, IRecoveryCodeRepository
    {
        private readonly ApiFlySicDbContext _context;
        public RecoveryCodeRepository(ApiFlySicDbContext context, INotificationService notifications) : base(context, notifications)
        {
            _context = context;
        }

        public async Task SaveCodeAsync(string email, string code, TimeSpan? expiration = null)
        {
            var existing = await _context.RecoveryCodes
                .Where(rc => rc.Email == email)
                .ToListAsync();

            _context.RecoveryCodes.RemoveRange(existing);

            var entity = new RecoveryCode
            (
                email,
                code,
                DateTime.UtcNow.Add(expiration ?? TimeSpan.FromMinutes(10))
            );

            _context.RecoveryCodes.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidateCodeAsync(string email, string code)
        {
            var entity = await _context.RecoveryCodes
                .FirstOrDefaultAsync(rc => rc.Email == email && rc.Code == code);

            if (entity == null || entity.IsExpired()) return false;

            return true;
        }

        public async Task ClearCodeAsync(string email)
        {
            var existing = await _context.RecoveryCodes
                .Where(rc => rc.Email == email)
                .ToListAsync();

            if (existing.Any())
            {
                _context.RecoveryCodes.RemoveRange(existing);
                await _context.SaveChangesAsync();
            }
        }
    }
}
