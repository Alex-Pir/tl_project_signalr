using PmsAgentManager.Exceptions;
using System.Collections.Concurrent;

namespace PmsAgentManager.Services
{
    public class Registry : IRegistry
    {
        private static readonly ConcurrentDictionary<int, string> StreamData = new();

        public void SetParameter(int guid, string parameter)
        {
            if (!StreamData.TryAdd(guid, parameter))
            {
                throw new RegistryException("Can not add data to Registry");
            }
        }

        public void RemoveParameter(int guid)
        {
            if (!StreamData.TryRemove(guid, out var result))
            {
                throw new RegistryException("Can not remove data from Registry");
            }
        }
        
        public string GetParameter(int guid)
        {
            StreamData.TryGetValue(guid, out var parameter);

            if (!string.IsNullOrEmpty(parameter))
            {
                RemoveParameter(guid);
            }
            
            return parameter ?? string.Empty;
        }
    }
}
