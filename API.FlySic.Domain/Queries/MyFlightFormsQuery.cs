using API.FlySic.Domain.Models.Response;
using MediatR;

namespace API.FlySic.Domain.Queries
{
    public class MyFlightFormsQuery : IRequest<List<MyFlightFormsResponseModel>>
    {
        public Guid UserId { get; set; }

        public MyFlightFormsQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}
