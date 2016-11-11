using System;

namespace Chat.Utils
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException(string message) : base(message)
        {
        }
    }
}
