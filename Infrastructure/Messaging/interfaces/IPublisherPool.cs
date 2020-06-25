using System.Collections.Generic;
using Messaging.Sending;

namespace Messaging.interfaces
{
    public interface IPublisherPool
    {
        void EnablePublishers();
        /// <summary>
        /// <TODO>improve interface. Only EnablePublishers needs to be available outside the package. Singleton? Make connection a factor or singleton. </TODO>
        /// Performance wise. With each MessageSender build a graph with publisherPool completely build and injected? Not ideal. Read-up on IServiceCollection, AddScoped, AddTransient, AddSingleton? within the thread rebuild? Do some thead management.
        /// </summary>
        /// <param name="messageToFindPublisherFor"></param>
        /// <returns></returns>
        IEnumerable<Publisher> GetPublishersForMessage(MessageBase messageToFindPublisherFor);
    }
}
