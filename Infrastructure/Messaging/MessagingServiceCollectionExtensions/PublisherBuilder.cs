using Messaging.Sending;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Messaging
{
    public class PublisherBuilder
    {
        private readonly string exchange;
        private readonly IServiceCollection services;
        private IEnumerable<Type> typesForPublisher = new Type[0];

        public PublisherBuilder(string queue, IServiceCollection services)
        {
            this.exchange = queue;
            this.services = services;
        }

        public PublisherBuilder ForMessagesInNamespaceOfType(Type scanNamespaceOfType)
        {
            var foundTypes =
                scanNamespaceOfType.Assembly.GetTypes()
                .Where(t =>
                t.IsClass
                && t.BaseType != null
                && !t.BaseType.IsGenericType
                && t.BaseType == typeof(MessageBase)
                && t.Namespace.Equals(scanNamespaceOfType.Namespace));
            typesForPublisher = typesForPublisher.Concat(foundTypes);

            return this;
        }

        public PublisherBuilder ForTypes(Type[] typesForPublisher)
        {
            this.typesForPublisher = this.typesForPublisher.Concat(typesForPublisher);
            return this;
        }

        public void Register()
        {
            services.AddScoped(_ => new PublisherOptions(exchange, this.typesForPublisher.ToArray()));
        }
    }
}