using Core.Utilities.MessageBrokers.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.MessageBrokerEventOperations
{
    public class EventFactory
    {

        public IEvent CreateEvent(dynamic @event, Type type)
        {
            if (type.Name == typeof(UserRegisteredEvent).Name)
            {
                return CreateUserRegisteredEvent(@event);
            }

            return null;
        }

        private IEvent CreateUserRegisteredEvent(dynamic @event)
        {
            IEvent userRegisteredEvent = new UserRegisteredEvent { UserEmail = @event.Email };
            return userRegisteredEvent;
        }
    }
}
