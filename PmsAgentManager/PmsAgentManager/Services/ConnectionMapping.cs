using System.Collections;

namespace PmsAgentManager.Services;

public class ConnectionMapping : IConnectionMapping
{
    private readonly Dictionary<string, int> _connections;

    public ConnectionMapping()
    {
        _connections = new Dictionary<string, int>();
    }
    
    public int Count => _connections.Count;

    public void Add(string connectionId, int guid)
    {
        lock (_connections)
        {
            if (_connections.TryGetValue(connectionId, out int connection))
            {
                _connections.Remove(connectionId);
            }
            
            _connections.Add(connectionId, guid);
        }
    }

    public int? GetConnection(string key)
    {
        if (_connections.TryGetValue(key, out int connection))
        {
            return connection;
        }

        return null;
    }

    public string GetConnectionKeyByValue(int value)
    {
        return _connections.FirstOrDefault(item => item.Value == value).Key;
    }
    
    public Dictionary<string, int> GetAllConnections()
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