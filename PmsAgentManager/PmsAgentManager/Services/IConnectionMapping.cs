namespace PmsAgentManager.Services;

public interface IConnectionMapping
{
    public void Add(string connectionId, int guid);
    public int? GetConnection(string key);
    public string GetConnectionKeyByValue(int value);
    public Dictionary<string, int> GetAllConnections();
    public void Remove(string connectionId);
}