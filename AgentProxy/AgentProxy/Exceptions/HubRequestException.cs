using System;

namespace AgentProxy.Exceptions
{
    public class HubRequestException : Exception
    {
        public HubRequestException(string message) : base(message)
        {
            
        }
    }
}