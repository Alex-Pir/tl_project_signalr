namespace PmsAgentManager.HttpApi
{
    public class HttpNpbApi : IHttpApi
    {

        private readonly string _url;
        private readonly string _page;
        
        public HttpNpbApi(string url,string page)
        {
            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(page))
            {
                throw new ArgumentException("Parameters error");
            }

            _url = url;
            _page = page;
        }
        
        public string GetData()
        {
            string result = "";
            using var client = new HttpClient();
            client.BaseAddress = new Uri(_url);
            client.DefaultRequestHeaders.Accept.Clear();
                
            //GET Method
            HttpResponseMessage response = client.GetAsync(_page).Result;
                
            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                Console.WriteLine("Internal server Error");
            }

            return result;
        }
    }
}