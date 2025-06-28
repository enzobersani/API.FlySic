using API.FlySic.Domain.Entities;
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
        Task<List<FlightForm>> GetByUserId(Guid userId);
    }
}
