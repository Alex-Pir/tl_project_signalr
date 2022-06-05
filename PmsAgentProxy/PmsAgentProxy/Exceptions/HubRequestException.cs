using System;

namespace PmsAgentProxy.Exceptions
{
    public class HubRequestException : Exception
    {
        public HubRequestException(string message) : base(message)
        {
            
        }
    }
}