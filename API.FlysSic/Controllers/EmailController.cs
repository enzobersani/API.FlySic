using API.FlySic.Controllers.Base;
using API.FlySic.Domain.Interfaces.Services;
using API.FlySic.Domain.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace API.FlySic.Controllers
{
    [ApiController]
    [Route("api/v1/send-email")]
    public class EmailController : ApiControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(INotificationService notification, IEmailService emailService) : base(notification)
        {
           _emailService = emailService;
        }

        //[HttpPost("new-user")]
        //[Consumes("multipart/form-data")]
        //public async Task<IActionResult> SendNewUserEmail([FromForm] NewUserRequest request)
        //{
        //    await _emailService.SendNewUserEmailAsync(request.Email, request);
        //    return Response(201);
        //}
    }
}
