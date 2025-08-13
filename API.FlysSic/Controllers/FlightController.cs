using API.FlySic.Controllers.Base;
using API.FlySic.Domain.Commands;
using API.FlySic.Domain.Interfaces.Context;
using API.FlySic.Domain.Models.Response;
using API.FlySic.Domain.Models.Response.Base;
using API.FlySic.Domain.Notifications;
using API.FlySic.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.FlySic.Controllers
{
    [Route("api/v1/flights")]
    public class FlightController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserContext _userContext;
        public FlightController(INotificationService notifications, IMediator mediator, IUserContext userContext) : base(notifications)
        {
            _mediator = mediator;
            _userContext = userContext;
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
        {
            if (request is null) return BadRequest("Body is required.");
            return Response(await _mediator.Send(request), 201);
        }

        /// <summary>
        /// Retorna lista de fichas de voo do piloto autenticado.
        /// </summary>
        /// <returns></returns>
        [HttpGet("my-flights/{status}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<MyFlightFormsResponseModel>), 200)]
        [ProducesResponseType(typeof(Notification), 400)]
        public async Task<IActionResult> GetMyFlights(int status)
        {
            var userId = _userContext.GetUserId();
            if (userId == Guid.Empty) return Unauthorized();
            return Response(await _mediator.Send(new MyFlightFormsQuery(userId, status)), 200);
        }

        /// <summary>
        /// Demonstra interesse no voo.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("flight-interest")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ExpressInterestCommand), 201)]
        [ProducesResponseType(typeof(Notification), 400)]
        public async Task<IActionResult> ExpressInterest([FromBody] ExpressInterestCommand command)
            => Response(await _mediator.Send(command), 201);

        /// <summary>
        /// Retorna lista de interessados em um voo específico.
        /// </summary>
        /// <param name="flightFormId"></param>
        /// <returns></returns>
        [HttpGet("interests/{flightFormId:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<FlightInterestResponse>), 200)]
        [ProducesResponseType(typeof(Notification), 400)]
        public async Task<IActionResult> GetMyFlights(Guid flightFormId)
            => Response(await _mediator.Send(new GetFlightInterestsQuery { FlightFormId = flightFormId }), 200);

        /// <summary>
        /// Lista fichas de voo de acordo com os filtros informados.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<SearchFlightFormsResponseModel>), 200)]
        [ProducesResponseType(typeof(Notification), 400)]
        public async Task<IActionResult> SearchFlights([FromQuery] SearchFlightFormsQuery request)
            => Response(await _mediator.Send(request), 200);

        /// <summary>
        /// Retorna ficha de voo pelo ID.
        /// </summary>
        /// <param name="flightFormId"></param>
        /// <returns></returns>
        [HttpGet("flight-form/{flightFormId:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SearchFlightFormsResponseModel), 200)]
        [ProducesResponseType(typeof(Notification), 400)]
        public async Task<IActionResult> GetFlightFormById(Guid flightFormId)
            => Response(await _mediator.Send(new GetFlightFormById(flightFormId)), 200);

        /// <summary>
        /// Piloto aceita interesse em seu voo.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("accept")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        [ProducesResponseType(typeof(Notification), 400)]
        public async Task<IActionResult> AcceptInterest([FromBody] AcceptFlightInterestCommand request)
        {
            var pilotId = _userContext.GetUserId();
            var command = new AcceptFlightInterestCommand
            {
                FlightFormId = request.FlightFormId,
                InterestId = request.InterestId,
                PilotId = pilotId
            };

            return Response(await _mediator.Send(command), 200);
        }

        /// <summary>
        /// Atualiza ficha de voo.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BaseUpdateResponse), 200)]
        [ProducesResponseType(typeof(Notification), 400)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateFlightFormCommand request)
        {
            request.Id = id;
            return Response(await _mediator.Send(request), 200);
        }

        /// <summary>
        /// Retorna o status da ficha de voo.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("status/{id:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(StatusFlightFormResponseModel), 200)]
        [ProducesResponseType(typeof(Notification), 400)]
        public async Task<IActionResult> GetStatus(Guid id)
            => Response(await _mediator.Send(new GetStatusFlightFormQuery(id)), 200);

        /// <summary>
        /// Finaliza a ficha de voo.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("finish")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BaseResponse), 201)]
        [ProducesResponseType(typeof(Notification), 400)]
        public async Task<IActionResult> Finish([FromBody] FinishFlightFormCommand request)
            => Response(await _mediator.Send(request), 200);

        /// <summary>
        /// Finaliza a ficha de voo.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("cancel")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BaseResponse), 201)]
        [ProducesResponseType(typeof(Notification), 400)]
        public async Task<IActionResult> Cancel([FromBody] CancelFlightFormCommand request)
            => Response(await _mediator.Send(request), 200);
    }
}