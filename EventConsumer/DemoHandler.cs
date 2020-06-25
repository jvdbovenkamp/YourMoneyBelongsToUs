using Events;
using Messaging.Receiving;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventConsumer
{
    public class DemoHandler : MessageHandlerBase<DemoDto>
    {
        protected override void HandleMessage(DemoDto message)
        {
            Console.WriteLine($"{DateTime.Now}: {message.DemoName}");
        }
    }
}
