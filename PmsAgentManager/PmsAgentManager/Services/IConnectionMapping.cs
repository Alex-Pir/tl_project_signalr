namespace PmsAgentManager.Services;

public interface IConnectionMapping
{
    public void Add(string connectionId, string guid);
    public string GetConnection(string key);
    public Dictionary<string, string> GetAllConnections();
    public void Remove(string connectionId);
}