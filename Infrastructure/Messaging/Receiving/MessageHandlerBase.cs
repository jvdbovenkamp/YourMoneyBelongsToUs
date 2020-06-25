using System.Runtime.InteropServices.ComTypes;

namespace Messaging.Receiving
{
    public abstract class MessageHandlerBase
    {
        internal abstract void HandleMessage(object message);
    }

    public abstract class MessageHandlerBase<TMessage> : MessageHandlerBase
    {
        internal override void HandleMessage(object message)
        {
            HandleMessage((TMessage)message);
        }

        protected abstract void HandleMessage(TMessage message);
    }
}
