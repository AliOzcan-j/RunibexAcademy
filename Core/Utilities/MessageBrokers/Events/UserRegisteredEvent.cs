using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.MessageBrokers.Events
{
    public class UserRegisteredEvent : IEvent
    {
        public string UserEmail { get; set; }
    }
}
