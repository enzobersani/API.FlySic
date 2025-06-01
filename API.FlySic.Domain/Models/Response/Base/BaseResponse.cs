using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FlySic.Domain.Models.Response.Base
{
    public class BaseResponse
    {
        public Guid Id { get; set; }
        private DateTime DateTime { get; set; }

        public BaseResponse()
        {
            DateTime = DateTime.Now;
        }
    }
}
