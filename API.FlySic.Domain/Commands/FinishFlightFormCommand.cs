using API.FlySic.Domain.Models.Response.Base;
using MediatR;

namespace API.FlySic.Domain.Commands
{
    public class FinishFlightFormCommand : IRequest<BaseResponse>
    {
        public Guid FlightFormId { get; set; }
        public Guid EvaluatorId { get; set; }
        public Guid EvaluatedId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}
