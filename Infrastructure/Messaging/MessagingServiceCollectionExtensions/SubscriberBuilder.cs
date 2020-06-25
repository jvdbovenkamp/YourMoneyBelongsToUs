using Messaging.Receiving;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Messaging
{
    public class SubscriberBuilder
    {
        private readonly string exchange;
        private readonly string queue;
        private readonly IServiceCollection serviceCollection;
        private IDictionary<Type, IEnumerable<Type>> messageToHandlerMappings;

        internal SubscriberBuilder(string exchange, string queue, IServiceCollection serviceCollection)
        {
            this.exchange = exchange;
            this.queue = queue;
            this.serviceCollection = serviceCollection;
        }

        public SubscriberBuilder WithHandlersFromAssembly(Assembly assemblyWithHandlers)
        {
            var instances =
                assemblyWithHandlers.GetTypes()
                .Where(t =>
                t.IsClass
                && t.BaseType != null
                && t.BaseType.IsGenericType
                && t.BaseType.GetGenericTypeDefinition() == typeof(MessageHandlerBase<>));

            messageToHandlerMappings = instances.GroupBy(t => t.BaseType.GenericTypeArguments.First()).ToDictionary(group => group.Key, group => group.AsEnumerable());
            return this;
        }

        public void Register()
        {
            // TODO: Check logic..how are things resolved?
            foreach (var handlerType in messageToHandlerMappings.Values.SelectMany(_ => _))
            {
                serviceCollection.AddScoped(handlerType);
            }
            // bit funky..collection of subscriberOptions is collected based on repetitive calls for different Subscribers.
            // improvements needed. SubscriberPool registration?
            serviceCollection.AddScoped(_ => new SubscriberOptions(this.exchange, this.queue, messageToHandlerMappings));
        }
    }
}