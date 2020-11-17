using System;
using Messaging;
using Messaging.interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace PaymentAccounts
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = (new ServiceCollection() as IServiceCollection)
                .AddMessaging(messaging =>
                    {
                        messaging.UseConnection("rabbitmq", "rabbitmquser", "5sdF=VKRnYU53TMT");
                        messaging
                            .AddSubscriber("default", "queue1")
                            .WithHandlersFromAssembly(typeof(DemoHandler).Assembly)
                            .Register();
                        messaging
                            .AddSubscriber("default", "queue2")
                            .WithHandlersFromAssembly(typeof(DemoHandler).Assembly)
                            .Register();
                        messaging
                            .AddPublisher("default")
                            .ForMessagesInNamespaceOfType(typeof(DemoDto))
                            .Register();
                    }
                ).BuildServiceProvider();
            
            var publisherPool = serviceProvider.GetRequiredService<IPublisherPool>();
            publisherPool.EnablePublishers();
            
            
        }
    }
}