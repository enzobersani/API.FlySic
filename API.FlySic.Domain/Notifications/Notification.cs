using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FlySic.Domain.Notifications
{
    public class Notification
    {
        public string Code { get; private set; }
        public string Message { get; private set; }
        public Notification(string code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
