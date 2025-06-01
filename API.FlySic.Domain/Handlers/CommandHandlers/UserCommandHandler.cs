using API.FlySic.Domain.Commands;
using API.FlySic.Domain.Entities;
using API.FlySic.Domain.Interfaces.Services;
using API.FlySic.Domain.Interfaces.UnitOfWork;
using API.FlySic.Domain.Models.Response.Base;
using API.FlySic.Domain.Notifications;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace API.FlySic.Domain.Handlers.CommandHandlers
{
    public class UserCommandHandler : IRequestHandler<NewUserCommand, BaseResponse>,
                                      IRequestHandler<UpdatePasswordCommand, BaseUpdateResponse>
    {
        private readonly INotificationService _notifications;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public UserCommandHandler(INotificationService notifications, IUnitOfWork unitOfWork, IEmailService emailService, IConfiguration configuration)
        {
            _notifications = notifications;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<BaseResponse> Handle(NewUserCommand request, CancellationToken cancellationToken)
        {
            await ValidateUser(request);
            if (_notifications.HasNotifications()) return new BaseResponse();

            var user = User.New(request);
            await _unitOfWork.UserRepository.AddAsync(user);

            var senderEmail = _configuration["EmailSettings:SenderEmail"];
            await _emailService.SendNewUserEmailAsync(senderEmail, request);

            return new BaseResponse
            {
                Id = user.Id,
            };
        }

        public async Task<BaseUpdateResponse> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);
            if (user is null)
            {
                _notifications.AddNotification("Handle", "Informed user does not exist.");
                return new BaseUpdateResponse();
            }

            if (!request.Password.Equals(request.ConfirmPassword))
            {
                _notifications.AddNotification("Handle", "Passwords differ.");
                return new BaseUpdateResponse();
            }

            user.UpdatePassword(request.Password);
            await _unitOfWork.CommitAsync();
            return new BaseUpdateResponse();
        }

        #region Private Methods
        private async Task ValidateUser(NewUserCommand request)
        {
            if (await _unitOfWork.UserRepository.IsEmailExists(request.Email))
                _notifications.AddNotification("ValidateUser", "This email is already registered.");

            if (await _unitOfWork.UserRepository.IsCpfExists(request.Cpf))
                _notifications.AddNotification("ValidateUser", "This cpf is already registered.");

            if (await _unitOfWork.UserRepository.IsPhoneExists(request.Phone))
                _notifications.AddNotification("ValidateUser", "This phone is already registered.");

        }
        #endregion
    }
}
