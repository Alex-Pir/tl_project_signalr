namespace PmsAgentManager.Services
{
    public class Registry
    {
        private static Registry? _instance;
        
        private static readonly Dictionary<string, string> StreamData = new();
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
            StreamData.Remove(guid);
            StreamData.Add(guid, parameter);
        }

        public string? GetParameter(string guid)
        {
            StreamData.TryGetValue(guid, out var parameter);
            
            return parameter;
        }
    }
}
