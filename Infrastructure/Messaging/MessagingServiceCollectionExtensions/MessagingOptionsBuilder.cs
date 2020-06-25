using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Generic;

namespace Messaging
{
    public class MessagingOptionsBuilder
    {
        private MessagingOptions options;
        public MessagingOptionsBuilder(IServiceCollection services)
        {
            Services = services;
            options = new MessagingOptions();
        }

        public MessagingOptions Options => options;

        private IServiceCollection Services { get; }
        public MessagingConnectionConfiguration Connection { get; private set; }

        public void UseConnection(string hostName, string userName, string password)
        {
            options.Connection = new MessagingConnectionConfiguration(hostName, userName, password);
        }

        public SubscriberBuilder AddSubscriber(string exchange, string queue)
        {
            return new SubscriberBuilder(exchange, queue, Services);
        }

        public PublisherBuilder AddPublisher(string queue)
        {
            return new PublisherBuilder(queue, Services);
        }
    }
}