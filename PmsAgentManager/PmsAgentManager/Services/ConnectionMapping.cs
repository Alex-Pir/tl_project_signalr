using System.Collections;

namespace PmsAgentManager.Services;

public class ConnectionMapping : IConnectionMapping<string>
{
    private readonly Dictionary<string, string> _connections =
        new Dictionary<string, string>();

    public int Count
    {
        get
        {
            return _connections.Count;
        }
    }

    public void Add(string key, string connectionId)
    {
        lock (_connections)
        {
            if (_connections.TryGetValue(key, out string? connection))
            {
                _connections.Remove(key);
            }
            
            _connections.Add(key, connectionId);
        }
    }

    public string GetConnection(string key)
    {
        if (_connections.TryGetValue(key, out string? connections))
        {
            return connections;
        }

        return string.Empty;
    }

    public IEnumerable<string> GetAllConnections()
    {
        return _connections.Values;
    }

    public void Remove(string key, string connectionId)
    {
        lock (_connections)
        {
            _connections.Remove(key);
        }
    }
}