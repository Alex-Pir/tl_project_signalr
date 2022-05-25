using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR.Transports;

namespace PmsAgentManagement.Services
{
    public class Registry
    {
        private static Registry _instance;
        
        private static readonly Dictionary<string, string> _streamData = new();
        private static object syncRoot = new();

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

        public void SetParameter(string guid, string parameter)
        {
            _streamData.Remove(guid);
            _streamData.Add(guid, parameter);
        }

        public string GetParameter(string guid)
        {
            _streamData.TryGetValue(guid, out var parameter);
            
            return parameter;
        }
    }
}
