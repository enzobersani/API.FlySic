using API.FlySic.Controllers.Base;
using API.FlySic.Domain.Commands;
using API.FlySic.Domain.Interfaces.Services;
using API.FlySic.Domain.Models.Response.Base;
using API.FlySic.Domain.Notifications;
using API.FlySic.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.FlySic.Controllers
{
    [Route("api/v1/users")]
    public class UserController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(INotificationService notifications, IMediator mediator) : base(notifications)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Criação de novo usuário.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("new-user")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(BaseResponse), 201)]
        [ProducesResponseType(typeof(Notification), 400)]
        public async Task<IActionResult> SendNewUserEmail([FromForm] NewUserCommand request)
            => Response(await _mediator.Send(request), 201);

        /// <summary>
        /// Atualizar senha.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("password")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        [ProducesResponseType(typeof(Notification), 400)]
        [Produces("application/json")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordCommand request)
            => Response(await _mediator.Send(request), 200);

        /// <summary>
        /// Atualiza o status do primeiro acesso do usuário.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("first-access")]
        [ProducesResponseType(typeof(BaseUpdateResponse), 200)]
        [ProducesResponseType(typeof(Notification), 400)]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateFirstAccess([FromBody] UpdateFirstAccessCommand request)
            => Response(await _mediator.Send(request), 200);

        /// <summary>
        /// Retorna se é o primeiro acesso do usuário.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("first-access-status/{userId:guid}")]
        [ProducesResponseType(typeof(bool), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> GetFirstAccessStatus([FromRoute] Guid userId)
            => Ok(await _mediator.Send(new GetFirstAccessStatusQuery(userId)));
    }
}
