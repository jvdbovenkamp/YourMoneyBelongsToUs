using System.Threading.Tasks;

namespace Messaging.interfaces
{
    public interface IMessagePublisher
    {
        Task Publish(MessageBase message);
    }
}