using System.Collections.Concurrent;

namespace PmsAgentManager.Services
{
    public class Registry : IRegistry
    {
        private static readonly ConcurrentDictionary<string, string> StreamData = new();

        public void SetParameter(string guid, string parameter)
        {
            if (!StreamData.TryAdd(guid, parameter))
            {
                throw new Exception("Can not add data to Registry");
            }
        }

        public void RemoveParameter(string guid)
        {
            if (!StreamData.TryRemove(guid, out var result))
            {
                throw new Exception("Can not remove data from Registry");
            }
        }
        
        public string? GetParameter(string guid)
        {
            StreamData.TryGetValue(guid, out var parameter);
            
            return parameter;
        }
    }
}
