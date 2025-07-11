using API.FlySic.Domain.Entities;
using API.FlySic.Domain.Interfaces.Repositories.Base;

namespace API.FlySic.Domain.Interfaces.Repositories
{
    public interface IFlightFormInterestRepository : IBaseRepository<FlightFormInterest>
    {
        Task<bool> ExistsAsync(Guid userId, Guid flightFormId);
        Task<List<FlightFormInterest>> GetByFlightFormIdAsync(Guid flightFormId);
        Task<FlightFormInterest?> GetByInterestId(Guid interestId, Guid flightFormId);
    }
}
