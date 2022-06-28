namespace PmsAgentManager.Services;

public interface IRegistry
{
    public void SetParameter(int guid, string parameter);
    public void RemoveParameter(int guid);
    public string GetParameter(int guid);
}