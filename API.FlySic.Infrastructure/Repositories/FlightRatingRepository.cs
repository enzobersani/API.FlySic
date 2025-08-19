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
    public class FlightRatingRepository : BaseRepository<FlightRating>, IFlightRatingRepository
    {
        private readonly ApiFlySicDbContext _context;
        public FlightRatingRepository(ApiFlySicDbContext context, INotificationService notifications) : base(context, notifications)
        {
            _context = context;
        }

        public async Task<FlightRating?> GetByFlightFormId(Guid flightFormId)
            => await _context.FlightRatings.AsNoTracking()
                                           .FirstOrDefaultAsync(x => x.FlightFormId == flightFormId);
    }
}
