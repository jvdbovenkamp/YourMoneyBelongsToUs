using Messaging.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Messaging.Receiving
{
    public class SubscriberPool : ISubscriberPool
    {
        private MessagingOptions _messagingOptions;
        private readonly IEnumerable<SubscriberOptions> configuredSubscribers;
        private readonly IServiceProvider serviceProvider;
        private MessageHandler[] _activeSubribers;

        public SubscriberPool(
            MessagingOptions messagingOptions
            , /* TODO. Fix SubscriberInfo rename to something like SubscriberConfiguration */ IEnumerable<SubscriberOptions> subscribers
            , IServiceProvider serviceProvider)
        {
            _messagingOptions = messagingOptions;
            configuredSubscribers = subscribers;
            // TODO: Figure out how to improve this part. Is ServiceProvider needed here => Factory as part of servicecollection
            this.serviceProvider = serviceProvider;
        }

        public void StartAll()
        {
            var messagingConnection = new MessagingConnection(_messagingOptions.Connection.HostName, _messagingOptions.Connection.UserName, _messagingOptions.Connection.Password);
            messagingConnection.Connect();

            _activeSubribers = configuredSubscribers.Select(_ => new MessageHandler(messagingConnection, _, serviceProvider)).ToArray();
            foreach (var subscriber in _activeSubribers)
            {
                subscriber.StartListening();
            }

        }

        public void StopAll()
        {
            // TODO
            // kill all
        }
    }
}
