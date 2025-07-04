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

        /// <summary>
        /// Receber código de recuperação
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("recovery-code")]
        public async Task<IActionResult> RecoveryCode([FromBody] RecoveryCodeCommand request)
            => Response(await _mediator.Send(request), 201);

        /// <summary>
        /// Valida código de recuperação
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("validate-recovery-code")]
        public async Task<IActionResult> ValidateRecoveryCode([FromBody] ValidateRecoveryCodeCommand request)
            => Response(await _mediator.Send(request), 200);

        /// <summary>
        /// Resetar senha
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand request)
            => Response(await _mediator.Send(request), 200);
    }
}
