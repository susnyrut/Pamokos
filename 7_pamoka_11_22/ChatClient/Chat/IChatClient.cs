using Chat.Model;
using System.Collections.Generic;

namespace Chat
{
    public interface IChatClient
    {
        void Register(string username, string password);

        void SendMessage(string to, string message);
        void SendFileMessage(string to, string fileName, byte[] content);

        void MarkMessageAsRead(int id);
        void DeleteMessage(int id);
        byte[] GetFileContent(int id);

        List<Message> GetUnreadMessages(int maxMessages = 20);
        List<Message> GetAllMessages(int maxMessages = 20);
    }
}