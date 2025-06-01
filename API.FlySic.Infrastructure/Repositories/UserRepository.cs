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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ApiFlySicDbContext _context;
        public UserRepository(ApiFlySicDbContext context, INotificationService notifications) : base(context, notifications)
        {
            _context = context;
        }

        public async Task<User?> GetByEmail(string email)
            => await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
        
        public async Task<bool> IsEmailExists(string email)
            => await _context.Users.AsNoTracking().AnyAsync(x => x.Email == email);
       
        public async Task<bool> IsCpfExists(string cpf)
            => await _context.Users.AsNoTracking().AnyAsync(x => x.Cpf == cpf);

        public async Task<bool> IsPhoneExists(string phone)
            => await _context.Users.AsNoTracking().AnyAsync(x => x.Phone == phone);
    }
}
