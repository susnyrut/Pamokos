namespace Chat.DTOs
{
    internal class ErrorMessage
    {
        public string message { get; set; }
        public string exceptionMessage { get; set; }
        public string exceptionType { get; set; }
        public string stackTrace { get; set; }
    }
}
