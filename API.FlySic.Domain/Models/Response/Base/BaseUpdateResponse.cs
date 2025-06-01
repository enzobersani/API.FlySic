using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FlySic.Domain.Models.Response.Base
{
    public class BaseUpdateResponse
    {
        public DateTime DateTime { get; set; } = DateTime.UtcNow;
    }
}
