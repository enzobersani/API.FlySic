using API.FlySic.Domain.Models.Response.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FlySic.Domain.Commands
{
    public class NewUserCommand : IRequest<BaseResponse>
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsAcceptedTerms { get; set; }
        public bool IsDonateHours { get; set; }
        public bool IsSearchHours { get; set; }
        public IFormFile Document { get; set; }
    }
}
