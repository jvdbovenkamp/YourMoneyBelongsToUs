using Messaging.interfaces;
using Messaging.Sending;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Messaging
{
    internal class PublisherPool : IPublisherPool
    {
        private readonly MessagingOptions messagingOptions;
        private readonly IEnumerable<PublisherOptions> publisherOptions;
        private Publisher[] enabledPublishers;

        public PublisherPool(
            MessagingOptions messagingOptions,
            IEnumerable<PublisherOptions> publisherOptions)
        {
            this.messagingOptions = messagingOptions;
            this.publisherOptions = publisherOptions;
        }

        public void EnablePublishers()
        {
            var messagingConnection = new MessagingConnection(messagingOptions.Connection.HostName, messagingOptions.Connection.UserName, messagingOptions.Connection.Password);
            messagingConnection.Connect();

            enabledPublishers = publisherOptions.Select(_ => new Publisher(messagingConnection, _)).ToArray();
            foreach (var publisher in enabledPublishers)
            {
                publisher.Enable();
            }
        }

        public IEnumerable<Publisher> GetPublishersForMessage(MessageBase messageToFindPublisherFor)
        {
            return enabledPublishers.Where(_ => _.IsPublisherForMessage(messageToFindPublisherFor)).ToList();
        }
    }
}
