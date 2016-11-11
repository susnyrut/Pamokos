using System;

namespace Chat.Model
{
    public class FileMessage : Message
    {
        public string FileName { get; private set; }
        public byte[] FileContent { get; private set; }

        public FileMessage(int id, string from, string text, DateTime date, bool isRead, byte[] content) : base(id, from, text, date, isRead, true)
        {
            FileName = text;
            FileContent = content;
        }
    }
}
