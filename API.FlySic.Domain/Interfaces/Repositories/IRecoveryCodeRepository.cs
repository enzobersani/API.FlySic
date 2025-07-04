using API.FlySic.Domain.Entities;
using API.FlySic.Domain.Interfaces.Repositories.Base;

namespace API.FlySic.Domain.Interfaces.Repositories
{
    public interface IRecoveryCodeRepository : IBaseRepository<RecoveryCode>
    {
        Task SaveCodeAsync(string email, string code, TimeSpan? expiration = null);
        Task<bool> ValidateCodeAsync(string email, string code);
        Task ClearCodeAsync(string email);
    }
}
