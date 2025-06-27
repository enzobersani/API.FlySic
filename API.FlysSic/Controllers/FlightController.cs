using API.FlySic.Controllers.Base;
using API.FlySic.Domain.Commands;
using API.FlySic.Domain.Models.Response.Base;
using API.FlySic.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.FlySic.Controllers
{
    [Route("api/v1/flights")]
    public class FlightController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        public FlightController(INotificationService notifications, IMediator mediator) : base(notifications)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cria nova ficha de voo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BaseResponse), 201)]
        [ProducesResponseType(typeof(Notification), 400)]
        public async Task<IActionResult> Post([FromBody] NewFlightFormCommand request)
            => Response(await _mediator.Send(request), 201);
    }
}
