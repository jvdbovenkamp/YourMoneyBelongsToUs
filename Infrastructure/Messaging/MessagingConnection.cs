using RabbitMQ.Client;
using System;

namespace Messaging
{
    public class MessagingConnection
    {
        private IConnection _connection;

        public MessagingConnection(string hostName, string userName, string password)
        {
            HostName = hostName;
            UserName = userName;
            Password = password;
        }
        public string HostName { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }

        public void Connect()
        {
            var factory = new ConnectionFactory() { HostName = HostName, UserName = UserName, Password = Password };

            _connection = factory.CreateConnection();
        }

        public IModel CreateModel()
        {
            if (_connection == null) throw new InvalidOperationException("Cannot create a model when there is no connection");
            return _connection.CreateModel();
        }
    }
}
