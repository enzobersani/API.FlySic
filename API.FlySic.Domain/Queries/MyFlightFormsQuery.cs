using API.FlySic.Domain.Enum;
using API.FlySic.Domain.Models.Response;
using MediatR;

namespace API.FlySic.Domain.Queries
{
    public class MyFlightFormsQuery : IRequest<List<MyFlightFormsResponseModel>>
    {
        public Guid UserId { get; set; }
        public FlightFormStatus Status { get; set; }

        public MyFlightFormsQuery(Guid userId, int status)
        {
            UserId = userId;
            Status = (FlightFormStatus)status;
        }
    }
}
