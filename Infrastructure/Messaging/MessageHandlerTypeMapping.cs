using System;

namespace Messaging
{
    public class MessageHandlerTypeMapping
    {
        public MessageHandlerTypeMapping(Type handlerType, Type messageType)
        {
            HandlerType = handlerType;
            MessageType = messageType;
        }

        public Type HandlerType { get; }
        public Type MessageType { get; }
    }
}