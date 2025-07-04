using API.FlySic.Domain.Entities;
using API.FlySic.Domain.Interfaces.Repositories.Base;

namespace API.FlySic.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetByEmail(string email);
        Task<bool> IsEmailExists(string email);
        Task<bool> IsCpfExists(string cpf);
        Task<bool> IsPhoneExists(string phone);
    }
}
