using Business.MessageBrokerEventOperations;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Core.Utilities.MessageBrokers;
using Core.Utilities.MessageBrokers.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Queue
{
    public class QueueAfterAspect : MethodInterception
    {
        private IMessageBroker _channel;
        private Type _type;

        public QueueAfterAspect(Type type)
        {
            _channel = ServiceTool.ServiceProvider.GetService<IMessageBroker>();
            _type = type;
        }

        protected override void OnAfter(IInvocation invocation)
        {//yalnızca email adresi gönderilmez, key:value şeklinde, mesaj objesine uygun bir obje ile publish metodunda serialize edilmeli. Aksi halde geri dönüşte objeye parse edemiyor
            dynamic arguments = invocation.Arguments.ElementAt(0);
            var @event = new EventFactory().CreateEvent(arguments, _type);
            
            _channel.Publish(@event);
        }
    }
}
