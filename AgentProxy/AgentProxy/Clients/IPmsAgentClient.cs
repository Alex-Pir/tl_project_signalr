namespace AgentProxy.Clients
{
    public interface IPmsAgentClient
    {
        string Call(string authorizationHeader, string requestBody, string agentUrl);
    }
}
