using System.Threading.Tasks;

namespace Messaging.interfaces
{
    public interface IMessageSender
    {
        void SendMessage(MessageBase message);
    }
}
