using Messaging.interfaces;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Messaging.Receiving
{
    /// <summary>
    /// Listen to incoming events on a specified Queue
    /// Proces the events with the correct handler
    /// </summary>
    public class MessageHandler : IMessageHandler
    {
        private readonly MessagingConnection _messagingConnection;
        private readonly SubscriberOptions _subscriberInfo;
        private readonly IServiceProvider serviceProvider;
        private IModel _model;
        private EventingBasicConsumer _consumer;

        public MessageHandler(MessagingConnection messagingConnection, SubscriberOptions subscriberInfo, IServiceProvider serviceProvider)
        {
            _messagingConnection = messagingConnection;
            _subscriberInfo = subscriberInfo;
            this.serviceProvider = serviceProvider;
        }

        public void StartListening()
        {
            _model = _messagingConnection.CreateModel();

            _model.ExchangeDeclare(exchange: _subscriberInfo.ExchangeName, ExchangeType.Fanout, durable: true, autoDelete: false);
            _model.QueueDeclare(queue: _subscriberInfo.QueueName, durable: true, exclusive: false, autoDelete: false);
            _model.QueueBind(queue: _subscriberInfo.QueueName, exchange: _subscriberInfo.ExchangeName, routingKey: "");

            _consumer = new EventingBasicConsumer(_model);
            _consumer.Received += Consumer_Received;
            _model.BasicConsume(_subscriberInfo.QueueName, true, _consumer);
        }

        private void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            // steps to consume this
            // 1. Deserialize the data into an object (determine type and deserialize)
            // 2. Use the MessageHandlerProvider to create a message handler. Need a new instance per message
            // 2.1. MessageHandlerProvider has knowledge of clrtype to handler
            // 2.1. Provider Gets an instance from DI container
            var typeName = System.Text.Encoding.UTF8.GetString((byte[])e.BasicProperties.Headers["ClrType"]);
            Type typeToDeserialize = Type.GetType(typeName);
            var content = e.Body.ToArray();
            var deserializedType = JsonSerializer.Deserialize(content, typeToDeserialize);

            var handlersToUse = _subscriberInfo.MessageToHandlerMappings[typeToDeserialize];

            foreach (var handlerToUse in handlersToUse)
            {
                var handler = (MessageHandlerBase)serviceProvider.CreateScope().ServiceProvider.GetRequiredService(handlerToUse);
                handler.HandleMessage(deserializedType);
            }
        }
    }
}
