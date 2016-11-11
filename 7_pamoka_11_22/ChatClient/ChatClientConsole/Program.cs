using Chat;
using Chat.Model;
using System.Collections.Generic;

namespace ChatClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IChatClient client = new ChatClient();

            client.Register("", "");
            client.SendMessage("", "");
            List<Message> messages = client.GetAllMessages(20);
        }
    }
}
