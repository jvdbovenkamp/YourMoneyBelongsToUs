namespace Messaging
{
    public class MessagingConnectionConfiguration
    {
        public MessagingConnectionConfiguration(string hostName, string userName, string password)
        {
            HostName = hostName;
            UserName = userName;
            Password = password;
        }
        public string HostName { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
    }
}