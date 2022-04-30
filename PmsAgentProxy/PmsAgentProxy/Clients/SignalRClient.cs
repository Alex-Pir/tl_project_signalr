using System;
using System.Threading;

namespace PmsAgentProxy.Clients
{
    public class SignalRClient : IClient
    {

        private readonly IProxy _proxy;

        public SignalRClient()
        {
            _proxy = new HubProxy();
        }

        public string Connection()
        {
           
            //_proxy.RegisterResponseHandler();
            _proxy.SendRequest("Hello");

            while (_proxy.Status != true)
            {
                Thread.Sleep(300);
            }

            return _proxy.ResultMessage;
        }
    }
}
