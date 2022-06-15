using System;
using System.Net.Http;

namespace PmsAgentProxy.Clients;

public class NpbClient : IPmsAgentClient
{
    public string Call(string authorizationHeader, string requestBody, string agentUrl)
    {
        string result = "";
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
                
        //GET Method
        HttpResponseMessage response = client.GetAsync(agentUrl).Result;
                
        if (response.IsSuccessStatusCode)
        {
            result = response.Content.ReadAsStringAsync().Result;
        }
        else
        {
            throw new Exception("Internal server Error");
        }
        
        return result;
    }
}