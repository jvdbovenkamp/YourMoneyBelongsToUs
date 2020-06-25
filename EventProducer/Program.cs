using System;
using System.Threading.Tasks;
using Events;
using Messaging;
using Messaging.interfaces;
using Messaging.Sending;
using Microsoft.Extensions.DependencyInjection;

namespace EventProducer
{
    class Program
    {
        private static string GetMessage(int counter)
        {
            return $"Hello World! {counter}";
        }

        static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(1000);
            Task.Run(async () =>
            {
                var serviceProvider = (new ServiceCollection() as IServiceCollection)
                    .AddMessaging(messaging =>
                    {
                        messaging.UseConnection("rabbitmq", "rabbitmquser", "5sdF=VKRnYU53TMT");
                        messaging
                            .AddPublisher("default")
                            .ForMessagesInNamespaceOfType(typeof(DemoDto))
                            .Register();
                    }).BuildServiceProvider();

                var publisherPool = serviceProvider.GetRequiredService<IPublisherPool>();
                publisherPool.EnablePublishers();

                var messageSender = serviceProvider.GetRequiredService<IMessageSender>();
                while (1 == 1)
                {
                    messageSender.SendMessage(new DemoDto { DemoName = $"Message ${DateTime.Now.ToLongTimeString()}" });
                    System.Threading.Thread.Sleep(50);
                }
            }).GetAwaiter().GetResult();
        }
    }
}
