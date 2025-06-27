using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FlySic.Domain.Interfaces.Context
{
    public interface IUserContext
    {
        Guid GetUserId();
        string? GetEmail();
        bool IsFirstAccess();
    }
}
