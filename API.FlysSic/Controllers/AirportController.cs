using API.FlySic.Controllers.Base;
using API.FlySic.Domain.Interfaces.Services;
using API.FlySic.Domain.Models;
using API.FlySic.Domain.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace API.FlySic.Controllers
{
    [Route("api/v1/airport")]
    public class AirportController : ApiControllerBase
    {
        private readonly IAirportService _airportService;
        public AirportController(INotificationService notifications, IAirportService airportService) : base(notifications)
        {
            _airportService = airportService;
        }

        /// <summary>
        /// Busca aeroporto por ICAO.
        /// </summary>
        /// <param name="icao"></param>
        /// <returns></returns>
        [HttpGet("{icao}")]
        [ProducesResponseType(typeof(List<AirportModel>), 201)]
        [ProducesResponseType(typeof(Notification), 400)]
        [Produces("application/json")]
        public IActionResult GetByIcao(string icao)
            => Response(_airportService.GetAirports(icao), 200);
    }
}
