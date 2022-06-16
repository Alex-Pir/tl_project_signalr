namespace PmsAgentProxy.Settings;

public class AgentSettings : BaseSettings
{
    private const string ServerSection = "agent";
    private const string ServerUrlKey = "url";
    public string Url { get; }

    public AgentSettings() : base()
    {
        Url = SettingsManager.ReadIni(ServerSection, ServerUrlKey);
    }
}