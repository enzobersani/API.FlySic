using API.FlySic.Domain.Commands;
using API.FlySic.Domain.Entities;
using API.FlySic.Domain.Enum;
using API.FlySic.Domain.Interfaces.Context;
using API.FlySic.Domain.Interfaces.Services;
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
                                        IRequestHandler<ExpressInterestCommand, BaseResponse>,
                                        IRequestHandler<AcceptFlightInterestCommand, BaseResponse>,
                                        IRequestHandler<UpdateFlightFormCommand, BaseUpdateResponse>
    {
        private readonly INotificationService _notificaion;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;
        private readonly IEmailService _emailService;

        public FlightCommandHandler(INotificationService notification, IUnitOfWork unitOfWork, IUserContext userContext, IEmailService emailService)
        {
            _notificaion = notification;
            _unitOfWork = unitOfWork;
            _userContext = userContext;
            _emailService = emailService;
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

            var flightForm = await _unitOfWork.FlightFormRepository.GetByIdAsync(request.FlightFormId);
            var pilotUserId = flightForm.UserId;
            if (pilotUserId == userId)
            {
                _notificaion.AddNotification("Handle", "Você não pode demonstrar interesse no seu proprio voo.");
                return new BaseResponse();
            }

            var interest = FlightFormInterest.Create(request.FlightFormId, userId);
            await _unitOfWork.FlightFormInterestRepository.AddAsync(interest);

            var pilotUser = await _unitOfWork.UserRepository.GetByIdAsync(pilotUserId);
            var interestedUser = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            await SendEmailAsync(pilotUser, interestedUser, flightForm);

            return new BaseResponse
            {
                Id = interest.Id,
            };
        }

        public async Task<BaseResponse> Handle(AcceptFlightInterestCommand request, CancellationToken cancellationToken)
        {
            var flight = await _unitOfWork.FlightFormRepository.GetByIdAsync(request.FlightFormId);
            if (flight is null)
            {
                _notificaion.AddNotification("Handle", "Voo não encontrado.");
                return new BaseResponse();
            }

            if (flight.UserId != request.PilotId)
                _notificaion.AddNotification("Handle", "Você não tem permissão para aceitar interessados nesta ficha.");

            if (flight.Status != FlightFormStatus.Aberta)
               _notificaion.AddNotification("Handle", "A ficha de voo não esta aberta.");

            if (_notificaion.HasNotifications()) return new BaseResponse();

            var interest = await _unitOfWork.FlightFormInterestRepository.GetByInterestId(request.InterestId, request.FlightFormId);
            if (interest is null || interest.FlightFormId != flight.Id)
            {
                _notificaion.AddNotification("Handle", "Interesse não encontrado ou não pertence a esta ficha de voo.");
                return new BaseResponse();
            }

            interest.Accept();

            var allInterests = await _unitOfWork.FlightFormInterestRepository.GetByFlightFormIdAsync(flight.Id);
            foreach (var other in allInterests.Where(i => i.InterestedUserId != request.InterestId))
            {
                other.Reject();
            }

            await _unitOfWork.CommitAsync();
            return new BaseResponse
            {
                Id = interest.Id,
            };
        }

        public async Task<BaseUpdateResponse> Handle(UpdateFlightFormCommand request, CancellationToken cancellationToken)
        {
            request.UserId = _userContext.GetUserId();
            if (request.UserId == Guid.Empty)
            {
                _notificaion.AddNotification("Unauthorized", "Não autorizado!");
                return new BaseUpdateResponse();
            }

            var flightForm = await _unitOfWork.FlightFormRepository.GetByIdAsync(request.Id);
            if (flightForm is null)
            {
                _notificaion.AddNotification("Handle", "Ficha de voo não encontrada.");
                return new BaseUpdateResponse();
            }

            if (flightForm.UserId != request.UserId)
            {
                _notificaion.AddNotification("Handle", "Você não tem permissão para atualizar esta ficha de voo.");
                return new BaseUpdateResponse();
            }

            flightForm.Update(request);
            await _unitOfWork.FlightFormRepository.UpdateAsync(flightForm);
            return new BaseUpdateResponse(); 
        }

        #region Private Methods
        private async Task SendEmailAsync(User pilot, User interested, FlightForm flightForm)
        {
            var departure = string.IsNullOrWhiteSpace(flightForm.DepartureAirport)
                ? flightForm.DepartureManualLocation
                : flightForm.DepartureAirport;

            var arrival = string.IsNullOrWhiteSpace(flightForm.ArrivalAirport)
                ? flightForm.ArrivalManualLocation
                : flightForm.ArrivalAirport;

            var subject = "Novo interessado no seu voo";
            var htmlBody = $@"
                <p>Olá {pilot.Name},</p>

                <p>O usuário <strong>{interested.Name}</strong> demonstrou interesse na sua ficha de voo.</p>

                <h3>🛫 Dados do voo:</h3>
                <ul>
                    <li><strong>Origem:</strong> {departure}</li>
                    <li><strong>Destino:</strong> {arrival}</li>
                    <li><strong>Data de Partida:</strong> {flightForm.DepartureDate:dd/MM/yyyy}</li>
                    <li><strong>Hora de Partida:</strong> {flightForm.DepartureTime:HH:mm}</li>
                    <li><strong>Data de Chegada:</strong> {flightForm.ArrivalDate:dd/MM/yyyy}</li>
                    <li><strong>Hora de Chegada:</strong> {flightForm.ArrivalTime:HH:mm}</li>
                    <li><strong>Aeronave:</strong> {flightForm.AircraftType}</li>
                    <li><strong>Pernoite:</strong> {(flightForm.HasOvernight ? "Sim" : "Não")}</li>
                </ul>

                {(string.IsNullOrWhiteSpace(flightForm.FlightComment) ? "" : $"<p><strong>Observações:</strong> {flightForm.FlightComment}</p>")}

                <p>Você pode acessar o sistema para visualizar mais detalhes e aceitar ou recusar o interessado.</p>

                <p>Atenciosamente,<br/>Equipe FlySIC</p>
            ";

            await _emailService.SendEmailAsync(pilot.Email, subject, htmlBody);
        }
        #endregion
    }
}
