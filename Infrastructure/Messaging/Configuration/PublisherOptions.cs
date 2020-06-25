using System;

namespace Messaging
{
    public class PublisherOptions
    {
        public PublisherOptions(string exchangeName, Type[] typesToPublish)
        {
            ExchangeName = exchangeName;
            TypesToPublish = typesToPublish;
        }

        public string ExchangeName { get; }
        public Type[] TypesToPublish { get; }
    }
}
