using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.MessageBrokers.Messages
{
    public class CreateEmailResponseMessage
    {
        public string UserEmail { get; set; }
        public string EmailRespone { get; set; }
    }
}
