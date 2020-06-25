using Messaging;
using EventConsumer;
using Microsoft.Extensions.DependencyInjection;
using Messaging.interfaces;
using Messaging.Receiving;

namespace EventConsumer
{
    public class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(1000);
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
                    }
                ).BuildServiceProvider();

            var hoster = serviceProvider.GetRequiredService<ISubscriberPool>();
            hoster.StartAll();

            while (1 == 1)
            {
                System.Threading.Tasks.Task.Delay(1000).Wait();
            }
        }
    }


    public class MoneyTransferHandled
    {

    }

    public class MoneyTransferHandledEventHandler : MessageHandlerBase<MoneyTransferHandled>
    {
        private readonly IMessageSender _messageSender;

        public MoneyTransferHandledEventHandler(IMessageSender messageSender)
        {
            _messageSender = messageSender;
        }

        protected override void HandleMessage(MoneyTransferHandled message)
        {
            // do stuffs
        }
    }
}
