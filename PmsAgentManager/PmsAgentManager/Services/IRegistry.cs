namespace PmsAgentManager.Services;

public interface IRegistry
{
    public void SetParameter(string guid, string parameter);
    public void RemoveParameter(string guid);
    public string GetParameter(string guid);
}