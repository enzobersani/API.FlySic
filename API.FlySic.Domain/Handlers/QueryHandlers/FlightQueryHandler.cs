using API.FlySic.Domain.Interfaces.UnitOfWork;
using API.FlySic.Domain.Models.Response;
using API.FlySic.Domain.Notifications;
using API.FlySic.Domain.Queries;
using MediatR;

namespace API.FlySic.Domain.Handlers.QueryHandlers
{
    public class FlightQueryHandler : IRequestHandler<MyFlightFormsQuery, List<MyFlightFormsResponseModel>>,
                                      IRequestHandler<GetFlightInterestsQuery, List<FlightInterestResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService _notification;

        public FlightQueryHandler(IUnitOfWork unitOfWork, INotificationService notification)
        {
            _unitOfWork = unitOfWork;
            _notification = notification;
        }

        public async Task<List<MyFlightFormsResponseModel>> Handle(MyFlightFormsQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
            if (user is null)
            {
                _notification.AddNotification("Handle", "Usuário informado não existe!");
                return new List<MyFlightFormsResponseModel>();
            }

            var flightForms = await _unitOfWork.FlightFormRepository.GetByUserId(user.Id);

            var result = flightForms.Select(f => new MyFlightFormsResponseModel
            {
                Id = f.Id,
                DepartureDate = f.DepartureDate,
                DepartureTime = f.DepartureTime,
                DepartureAirport = f.DepartureAirport,
                DepartureManualLocation = f.DepartureManualLocation,
                ArrivalDate = f.ArrivalDate,
                ArrivalTime = f.ArrivalTime,
                ArrivalAirport = f.ArrivalAirport,
                ArrivalManualLocation = f.ArrivalManualLocation,
                AircraftType = f.AircraftType,
                FlightComment = f.FlightComment,
                HasOvernight = f.HasOvernight
            }).ToList();

            return result;
        }

        public async Task<List<FlightInterestResponse>> Handle(GetFlightInterestsQuery request, CancellationToken cancellationToken)
        {
            var interests = await _unitOfWork.FlightFormInterestRepository.GetByFlightFormIdAsync(request.FlightFormId);

            return interests.Select(i => new FlightInterestResponse
            {
                UserId = i.InterestedUser.Id,
                Name = i.InterestedUser.Name,
                Email = i.InterestedUser.Email,
            }).ToList();
        }
    }
}
