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
    public class FlightFormRepository : BaseRepository<FlightForm>, IFlightFormRepository
    {
        private readonly ApiFlySicDbContext _context;
        public FlightFormRepository(ApiFlySicDbContext context, INotificationService notifications) : base(context, notifications)
        {
            _context = context;
        }
    }
}
