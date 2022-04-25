using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PmsAgentManagement.HttpApi
{
    public class HttpNpbApi : IHttpApi
    {
        private const string ResponseFormat = "xml";

        private readonly HttpClient _client = new HttpClient();
        
        public string GetData()
        {
            string result = "";
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.nbp.pl/api/");
                client.DefaultRequestHeaders.Accept.Clear();
                //GET Method
                
                HttpResponseMessage response = client.GetAsync($"exchangerates/tables/A/?format={ResponseFormat}").Result;
                
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