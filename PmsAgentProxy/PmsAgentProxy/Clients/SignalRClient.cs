using System;

namespace PmsAgentProxy.Clients
{
    public class SignalRClient
    {

        private IProxy _proxy;

        public SignalRClient()
        {
            _proxy = new HubProxy();
        }

        public void connection()
        {
            try
            {
                _proxy.RegisterResponseHandler();
                _proxy.SendRequest("Hello");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        } 
    }
}
