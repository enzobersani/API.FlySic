using API.FlySic.Domain.Interfaces.Repositories;
using API.FlySic.Domain.Interfaces.UnitOfWork;
using API.FlySic.Domain.Notifications;
using API.FlySic.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FlySic.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApiFlySicDbContext _context;
        private readonly INotificationService _notifications;
        private IUserRepository _userRespository;
        private IFlightFormRepository _flightFormRepository;
        private IFlightFormInterestRepository _flightFormInterestRepository;

        public UnitOfWork(ApiFlySicDbContext context, INotificationService notifications)
        {
            _context = context;
            _notifications = notifications;
        }

        public IUserRepository UserRepository
            => _userRespository ??= new UserRepository(_context, _notifications);
        public IFlightFormRepository FlightFormRepository
            => _flightFormRepository ??= new FlightFormRepository(_context, _notifications);
        public IFlightFormInterestRepository FlightFormInterestRepository
            => _flightFormInterestRepository ??= new FlightFormInterestRepository(_context, _notifications);

        public async Task<bool> CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _notifications.AddNotification("CommitAsync", $"Ocorreu um erro ao salvar {ex.ToString()}");
                return false;
            }

            return true;
        }
    }
}
