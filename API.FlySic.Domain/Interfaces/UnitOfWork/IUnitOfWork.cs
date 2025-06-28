using API.FlySic.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FlySic.Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();
        IUserRepository UserRepository { get; }
        IFlightFormRepository FlightFormRepository { get; }
        IFlightFormInterestRepository FlightFormInterestRepository { get; }
    }
}
