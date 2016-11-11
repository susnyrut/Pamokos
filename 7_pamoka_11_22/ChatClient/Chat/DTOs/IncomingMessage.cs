using System;

namespace Chat.DTOs
{
    internal class IncomingMessage
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public bool IsRead { get; set; }
        public bool IsFile { get; set; }
    }
}
