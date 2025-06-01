using API.FlySic.Controllers.Base;
using API.FlySic.Domain.Commands;
using API.FlySic.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.FlySic.Controllers
{
    [Route("api/v1/auth")]
    public class AuthController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(INotificationService notifications, IMediator mediator) : base(notifications)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Realizar login
        /// </summary>
        /// <param name="request"></param>
        /// <returns>token JWT</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthCommand request)
            => Response(await _mediator.Send(request), 200);
    }
}
