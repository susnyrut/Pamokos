using System;

namespace Chat.Model
{
    public class Message
    {
        public int Id { get; }
        public string From { get; }
        public string Text { get; }
        public DateTime Date { get; }
        public bool IsRead { get; }
        public bool IsFile { get; }

        internal Message(int id, string from, string text, DateTime date, bool isRead, bool isFile)
        {
            Id = id;
            From = from;
            Text = text;
            Date = date;
            IsRead = isRead;
            IsFile = isFile;
        }
    }
}
