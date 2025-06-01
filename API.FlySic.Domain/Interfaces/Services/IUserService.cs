using API.FlySic.Domain.Commands;
using API.FlySic.Domain.Models.Response.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FlySic.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<BaseResponse> Create(NewUserCommand request);
    }
}
