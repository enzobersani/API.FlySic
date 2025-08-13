using API.FlySic.Domain.Models.Response.Base;
using MediatR;

namespace API.FlySic.Domain.Commands
{
    public class CancelFlightFormCommand : IRequest<BaseResponse>
    {
        public Guid FlightFormId { get; set; }
        public Guid UserId { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}
