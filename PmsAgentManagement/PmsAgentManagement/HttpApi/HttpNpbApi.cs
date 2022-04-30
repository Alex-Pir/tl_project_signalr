using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Configuration;
using PmsAgentManagement.Services.RemoteServices;

namespace PmsAgentManagement.HttpApi
{
    public class HttpNpbApi : IHttpApi
    {

        private readonly ServiceTLConfigSection _service;

        public HttpNpbApi()
        {
            RemoteServicesConfigGroup group = (RemoteServicesConfigGroup)WebConfigurationManager.OpenWebConfiguration("/")
                .GetSectionGroup("remoteServices");

            if (group == null)
            {
                throw new Exception("Service configuration not found");
            }
            
            _service = (ServiceTLConfigSection)group.Sections.Get("serviceTL");
        }
        
        public string GetData()
        {
            string result = "";
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_service.Url);
                client.DefaultRequestHeaders.Accept.Clear();
                
                //GET Method
                HttpResponseMessage response = client.GetAsync(_service.Page).Result;
                
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }
            }

            return result;
        }
    }
}