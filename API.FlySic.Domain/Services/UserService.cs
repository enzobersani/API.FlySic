using API.FlySic.Domain.Commands;
using API.FlySic.Domain.Interfaces.Services;
using API.FlySic.Domain.Models.Response.Base;
using API.FlySic.Domain.Notifications;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FlySic.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly INotificationService _notification;
        private readonly IEmailService _emailService;

        public UserService(INotificationService notification, IEmailService emailService)
        {
            _notification = notification;
            _emailService = emailService;
        }

        public async Task<BaseResponse> Create(NewUserCommand request)
        {
            await _emailService.SendNewUserEmailAsync(request.Email, request);

            return new BaseResponse
            {
                Id = Guid.NewGuid()
            };
        }
    }
}
