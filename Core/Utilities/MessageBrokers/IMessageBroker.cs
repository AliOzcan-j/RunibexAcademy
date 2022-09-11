using Core.Utilities.MessageBrokers.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.MessageBrokers
{
    public interface IMessageBroker
    {
        public void Publish<T>(T @event);
    }
}
