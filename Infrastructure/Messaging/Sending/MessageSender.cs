using Messaging.interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Sending
{
    internal class MessageSender : IMessageSender
    {
        private readonly IPublisherPool availablePublishers;

        public MessageSender(IPublisherPool availablePublishers)
        {
            this.availablePublishers = availablePublishers;
        }

        public void SendMessage(MessageBase message)
        {
            var publishersToSendMessage = availablePublishers.GetPublishersForMessage(message);
            foreach (var publisher in publishersToSendMessage)
            {
                publisher.Publish(message);
            }
        }
    }
}
