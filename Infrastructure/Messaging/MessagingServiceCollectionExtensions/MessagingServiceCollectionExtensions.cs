using Messaging.interfaces;
using Messaging.Receiving;
using Messaging.Sending;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Messaging
{
    public static class MessagingServiceCollectionExtensions
    {
        public static IServiceCollection AddMessaging(this IServiceCollection services, Action<MessagingOptionsBuilder> messagingOptions)
        {
            var messagingOptionsBuilder = new MessagingOptionsBuilder(services);
            messagingOptions?.Invoke(messagingOptionsBuilder);
            services.AddScoped<MessagingOptions>(_ => messagingOptionsBuilder.Options);
            services.AddScoped<ISubscriberPool, SubscriberPool>();
            services.AddScoped<IPublisherPool, PublisherPool>();
            services.AddScoped<IMessageSender, MessageSender>();
            return services;
        }
    }
}
