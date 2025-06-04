using API.FlySic.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FlySic.Domain.Interfaces.Services
{
    public interface IAirportService
    {
        List<AirportModel>? GetAirports(string icao);
    }
}
