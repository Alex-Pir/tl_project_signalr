using System.Collections;

namespace PmsAgentManager.Services;

public class ConnectionMapping : IConnectionMapping
{
    private readonly Dictionary<string, string> _connections;

    public ConnectionMapping()
    {
        _connections = new Dictionary<string, string>();
    }
    
    public int Count => _connections.Count;

    public void Add(string connectionId, string guid)
    {
        lock (_connections)
        {
            if (_connections.TryGetValue(connectionId, out string? connection))
            {
                _connections.Remove(connectionId);
            }
            
            _connections.Add(connectionId, guid);
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

    public string GetConnectionKeyByValue(string value)
    {
        return _connections.FirstOrDefault(item => item.Value == value).Key;
    }
    
    public Dictionary<string, string> GetAllConnections()
    {
        return _connections;
    }

    public void Remove(string connectionId)
    {
        lock (_connections)
        {
            _connections.Remove(connectionId);
        }
    }
}