using System;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR.Transports;

namespace PmsAgentManagement.Services
{
    public class Registry
    {
        private static Registry _instance;
        
        private static readonly Dictionary<string, ITrackingConnection> _userConnections = new Dictionary<string, ITrackingConnection>();
        private static object syncRoot = new Object();

        private Registry() {}
        
        public static Registry GetInstance()
        {
            if (_instance != null) 
            {
                return _instance;
            }
            
            lock (syncRoot)
            {
                _instance ??= new Registry();
            }

            return _instance;
        }

        public void SetConnection(string guid, ITrackingConnection connection)
        {
            if (CheckConnection(connection))
            {
                _userConnections.Remove(guid);
                _userConnections.Add(guid, connection);
            }
        }

        public ITrackingConnection GetConnection(string guid)
        {
            _userConnections.TryGetValue(guid, out var connection);

            CheckConnection(connection);

            return connection;
        }

        private bool CheckConnection(ITrackingConnection connection)
        {
            if (connection is not { IsAlive: true })
            {
                throw new Exception("Connection is died");
            }

            return true;
        }
    }
}
