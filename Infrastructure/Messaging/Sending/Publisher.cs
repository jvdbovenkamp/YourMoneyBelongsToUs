using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Messaging.interfaces;
using System.Runtime.CompilerServices;

namespace Messaging.Sending
{
    public class Publisher : IMessagePublisher
    {
        private readonly MessagingConnection _messagingConnection;
        private readonly PublisherOptions _publisherOptions;
        private IModel _model;
        private IBasicProperties _properties;

        public Publisher(MessagingConnection messagingConnection, PublisherOptions publisherOptions)
        {
            _messagingConnection = messagingConnection;
            _publisherOptions = publisherOptions;
        }

        internal void Enable()
        {
            _model = _messagingConnection.CreateModel();

            _model.ExchangeDeclare(exchange: _publisherOptions.ExchangeName, ExchangeType.Fanout, durable: true, autoDelete: false);
        }

        internal bool IsPublisherForMessage(MessageBase message)
        {
            return _publisherOptions.TypesToPublish.Contains(message.GetType());
        }

        public Task Publish(MessageBase message)
        {
            return Task.Run(() =>
            {
                // check message object type? attribute based check?
                // TODO: create a serialzer / deserializer
                var messageBody = JsonSerializer.SerializeToUtf8Bytes(message);

                IBasicProperties messageProperties = _model.CreateBasicProperties();
                messageProperties.Headers = new Dictionary<string, object>();
                messageProperties.Headers.Add("ClrType", message.GetType().AssemblyQualifiedName);
                _model.BasicPublish(exchange: _publisherOptions.ExchangeName,
                    routingKey: "",
                    messageProperties,
                    body: messageBody);
            });
        }
    }
}
