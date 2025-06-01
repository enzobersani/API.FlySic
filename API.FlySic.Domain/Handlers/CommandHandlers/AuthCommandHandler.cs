using API.FlySic.Domain.Commands;
using API.FlySic.Domain.Interfaces.UnitOfWork;
using API.FlySic.Domain.Models.Response;
using API.FlySic.Domain.Notifications;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FlySic.Domain.Handlers.CommandHandlers
{
    public class AuthCommandHandler : IRequestHandler<AuthCommand, AuthResponseModel>
    {
        private readonly INotificationService _notifications;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public AuthCommandHandler(INotificationService notifications, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _notifications = notifications;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<AuthResponseModel> Handle(AuthCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByEmail(request.Email);
            if (user is null || !user.VerifyPassword(request.Password))
            {
                _notifications.AddNotification("Handle", "Invalid email or password.");
                return new AuthResponseModel();
            }

            return new AuthResponseModel
            {
                Token = "TOKEN_TESTE"
            };
        }
    }
}
