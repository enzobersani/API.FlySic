using API.FlySic.Domain.Entities;
using API.FlySic.Domain.Enum;
using API.FlySic.Domain.Interfaces.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FlySic.Domain.Interfaces.Repositories
{
    public interface IFlightFormRepository : IBaseRepository<FlightForm>
    {
        Task<List<FlightForm>> GetByPilotId(Guid userId);
        Task<List<FlightForm>> GetByUserAndStatus(Guid userId, FlightFormStatus status);
        Task<List<FlightForm>> GetByAcceptedUserId(Guid userId);
    }
}
