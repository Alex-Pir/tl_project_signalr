namespace PmsAgentManager.Services;

public interface IConnectionMapping<in T>
{
    public void Add(T key, string connectionId);
    public string GetConnection(T key);
    public IEnumerable<string> GetAllConnections();
    public void Remove(T key, string connectionId);
}