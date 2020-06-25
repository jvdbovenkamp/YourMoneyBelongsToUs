using System;
using System.Collections.Generic;

namespace Messaging
{
    public class SubscriberOptions
    {
        public SubscriberOptions(string exchangeName, string queueName, IDictionary<Type, IEnumerable<Type>> messageToHandlerMappings)
        {
            ExchangeName = exchangeName;
            QueueName = queueName;
            MessageToHandlerMappings = messageToHandlerMappings;
        }

        public IDictionary<Type, IEnumerable<Type>> MessageToHandlerMappings { get; }
        public string ExchangeName { get; }
        public string QueueName { get; }
    }
}
