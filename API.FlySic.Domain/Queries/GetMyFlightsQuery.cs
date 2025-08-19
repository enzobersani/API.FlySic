using API.FlySic.Domain.Models.Response;
using MediatR;

namespace API.FlySic.Domain.Queries;

public class GetMyFlightsQuery : IRequest<List<MyFlightsResponseModel>>
{
    public Guid UserId { get; set; }
}