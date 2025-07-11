using API.FlySic.Domain.Enum;
using API.FlySic.Domain.Interfaces.Context;
using API.FlySic.Domain.Interfaces.UnitOfWork;
using API.FlySic.Domain.Models.Response;
using API.FlySic.Domain.Notifications;
using API.FlySic.Domain.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.FlySic.Domain.Handlers.QueryHandlers
{
    public class FlightQueryHandler : IRequestHandler<MyFlightFormsQuery, List<MyFlightFormsResponseModel>>,
                                      IRequestHandler<GetFlightInterestsQuery, List<FlightInterestResponse>>,
                                      IRequestHandler<SearchFlightFormsQuery, List<SearchFlightFormsResponseModel>>,
                                      IRequestHandler<GetFlightFormById, SearchFlightFormsResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService _notification;
        private readonly IUserContext _userContext;

        public FlightQueryHandler(IUnitOfWork unitOfWork, INotificationService notification, IUserContext userContext)
        {
            _unitOfWork = unitOfWork;
            _notification = notification;
            _userContext = userContext;
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
                Phone = i.InterestedUser.Phone,
                IsAccepted = i.Status == FlightFormInterestStatus.Accepted
            }).ToList();
        }

        public async Task<List<SearchFlightFormsResponseModel>> Handle(SearchFlightFormsQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContext.GetUserId();

            var query = _unitOfWork.FlightFormRepository
                .Query(include => include.Include(f => f.User))
                .AsQueryable();

            if (userId != Guid.Empty)
                query = query.Where(f => f.UserId != userId);

            if (request.DepartureDate.HasValue)
                query = query.Where(f => f.DepartureDate.Date == request.DepartureDate.Value.Date);

            if (request.ArrivalDate.HasValue)
                query = query.Where(f => f.ArrivalDate.Date == request.ArrivalDate.Value.Date);

            if (!string.IsNullOrWhiteSpace(request.DepartureLocation))
                query = query.Where(f =>
                    (f.DepartureAirport ?? "").Contains(request.DepartureLocation) ||
                    (f.DepartureManualLocation ?? "").Contains(request.DepartureLocation));

            if (!string.IsNullOrWhiteSpace(request.ArrivalLocation))
                query = query.Where(f =>
                    (f.ArrivalAirport ?? "").Contains(request.ArrivalLocation) ||
                    (f.ArrivalManualLocation ?? "").Contains(request.ArrivalLocation));

            var flights = await query
                .OrderBy(f => f.DepartureDate)
                .ToListAsync(cancellationToken);

            var flightFormIds = flights.Select(f => f.Id).ToList();

            var userInterests = await _unitOfWork.FlightFormInterestRepository
                .Query()
                .Where(i => i.InterestedUserId == userId && flightFormIds.Contains(i.FlightFormId))
                .Select(i => i.FlightFormId)
                .ToListAsync(cancellationToken);

            var result = flights.Select(f => new SearchFlightFormsResponseModel
            {
                Id = f.Id,
                DepartureDate = f.DepartureDate,
                DepartureAirport = f.DepartureAirport,
                DepartureManualLocation = f.DepartureManualLocation,
                ArrivalDate = f.ArrivalDate,
                ArrivalAirport = f.ArrivalAirport,
                ArrivalManualLocation = f.ArrivalManualLocation,
                AircraftType = f.AircraftType,
                HasOvernight = f.HasOvernight,
                FlightComment = f.FlightComment,
                ArrivalTime = f.ArrivalTime,
                DepartureTime = f.DepartureTime,
                Pilot = new PilotResponseModel
                {
                    Id = f.User.Id,
                    Name = f.User.Name,
                    Email = f.User.Email,
                    Phone = f.User.Phone
                },
                UserAlreadyInterested = userInterests.Contains(f.Id)
            }).ToList();

            return result;
        }


        public async Task<SearchFlightFormsResponseModel> Handle(GetFlightFormById request, CancellationToken cancellationToken)
        {
            var userId = _userContext.GetUserId();

            var flightForm = await _unitOfWork.FlightFormRepository
                .Query(include => include.Include(f => f.User))
                .FirstOrDefaultAsync(f => f.Id == request.FlightFormId, cancellationToken);

            if (flightForm == null)
            {
                _notification.AddNotification("GetFlightFormById", "Ficha de voo não encontrada.");
                return new SearchFlightFormsResponseModel();
            }

            var userAlreadyInterested = await _unitOfWork.FlightFormInterestRepository
                .ExistsAsync(userId, request.FlightFormId);

            return new SearchFlightFormsResponseModel
            {
                Id = flightForm.Id,
                DepartureDate = flightForm.DepartureDate,
                DepartureTime = flightForm.DepartureTime,
                DepartureAirport = flightForm.DepartureAirport,
                DepartureManualLocation = flightForm.DepartureManualLocation,
                ArrivalDate = flightForm.ArrivalDate,
                ArrivalTime = flightForm.ArrivalTime,
                ArrivalAirport = flightForm.ArrivalAirport,
                ArrivalManualLocation = flightForm.ArrivalManualLocation,
                AircraftType = flightForm.AircraftType,
                FlightComment = flightForm.FlightComment,
                HasOvernight = flightForm.HasOvernight,
                Pilot = new PilotResponseModel
                {
                    Id = flightForm.User.Id,
                    Name = flightForm.User.Name,
                    Email = flightForm.User.Email,
                    Phone = flightForm.User.Phone
                },
                UserAlreadyInterested = userAlreadyInterested
            };
        }
    }
}
