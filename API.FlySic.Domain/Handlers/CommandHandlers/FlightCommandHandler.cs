using API.FlySic.Domain.Commands;
using API.FlySic.Domain.Entities;
using API.FlySic.Domain.Interfaces.Context;
using API.FlySic.Domain.Interfaces.UnitOfWork;
using API.FlySic.Domain.Models.Response.Base;
using API.FlySic.Domain.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FlySic.Domain.Handlers.CommandHandlers
{
    public class FlightCommandHandler : IRequestHandler<NewFlightFormCommand, BaseResponse>,
                                        IRequestHandler<ExpressInterestCommand, BaseResponse>
    {
        private readonly INotificationService _notificaion;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public FlightCommandHandler(INotificationService notification, IUnitOfWork unitOfWork, IUserContext userContext)
        {
            _notificaion = notification;
            _unitOfWork = unitOfWork;
            _userContext = userContext;
        }
        public async Task<BaseResponse> Handle(NewFlightFormCommand request, CancellationToken cancellationToken)
        {
            request.UserId = _userContext.GetUserId();
            if (request.UserId == Guid.Empty)
            {
                _notificaion.AddNotification("Unauthorized", "Não autorizado!");
                return new BaseResponse();
            }

            var flightForm = FlightForm.New(request);
            await _unitOfWork.FlightFormRepository.AddAsync(flightForm);
            return new BaseResponse
            {
                Id = flightForm.Id,
            };
        }

        public async Task<BaseResponse> Handle(ExpressInterestCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContext.GetUserId();
            if (await _unitOfWork.FlightFormInterestRepository.ExistsAsync(userId, request.FlightFormId))
            {
                _notificaion.AddNotification("Handle", "Você já demonstrou interesse neste voo.");
                return new BaseResponse();
            }

            var pilotUserId = (await _unitOfWork.FlightFormRepository.GetByIdAsync(request.FlightFormId)).UserId;
            if (pilotUserId == userId)
            {
                _notificaion.AddNotification("Handle", "Você não pode demonstrar interesse no seu proprio voo.");
                return new BaseResponse();
            }

            var interest = FlightFormInterest.Create(request.FlightFormId, userId);
            await _unitOfWork.FlightFormInterestRepository.AddAsync(interest);

            return new BaseResponse
            {
                Id = interest.Id,
            };
        }
    }
}
