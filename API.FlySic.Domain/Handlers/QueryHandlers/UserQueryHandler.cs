using API.FlySic.Domain.Interfaces.UnitOfWork;
using API.FlySic.Domain.Notifications;
using API.FlySic.Domain.Queries;
using MediatR;

namespace API.FlySic.Domain.Handlers.QueryHandlers;

public class UserQueryHandler : IRequestHandler<GetFirstAccessStatusQuery, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly INotificationService _notification;

    public UserQueryHandler(IUnitOfWork unitOfWork, INotificationService notification)
    {
        _unitOfWork = unitOfWork;
        _notification = notification;
    }

    public async Task<bool> Handle(GetFirstAccessStatusQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
        if (user == null)
            return false;

        return user.IsFirstAccess;
    }
}
