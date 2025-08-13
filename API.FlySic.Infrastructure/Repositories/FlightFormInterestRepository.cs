using API.FlySic.Domain.Entities;
using API.FlySic.Domain.Enum;
using API.FlySic.Domain.Interfaces.Repositories;
using API.FlySic.Domain.Notifications;
using API.FlySic.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace API.FlySic.Infrastructure.Repositories
{
    public class FlightFormInterestRepository : BaseRepository<FlightFormInterest>, IFlightFormInterestRepository
    {
        private readonly ApiFlySicDbContext _context;
        public FlightFormInterestRepository(ApiFlySicDbContext context, INotificationService notifications) : base(context, notifications)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(Guid userId, Guid flightFormId)
        {
            return await _context.FlightFormInterests
                .AnyAsync(x => x.InterestedUserId == userId && x.FlightFormId == flightFormId);
        }

        public async Task<List<FlightFormInterest>> GetByFlightFormIdAsync(Guid flightFormId)
        {
            return await _context.FlightFormInterests
                .Include(x => x.InterestedUser)
                .Where(x => x.FlightFormId == flightFormId)
                .ToListAsync();
        }

        public async Task<FlightFormInterest?> GetByInterestId(Guid interestId, Guid flightFormId)
        {
            return await _context.FlightFormInterests
                .FirstOrDefaultAsync(x => x.InterestedUserId == interestId && x.FlightFormId == flightFormId);
        }

        public async Task<Guid?> GetEvaluated(Guid flightFormId)
        {
            return (await _context.FlightFormInterests
                    .FirstOrDefaultAsync(x => x.Status == FlightFormInterestStatus.Accepted))
                    ?.InterestedUserId;
        }
    }
}
