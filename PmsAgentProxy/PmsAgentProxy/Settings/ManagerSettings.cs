namespace PmsAgentProxy.Settings;

public class ManagerSettings : BaseSettings
{
    private const string ServerSection = "manager";
    private const string ServerUrlKey = "url";
    public string Url { get; }

    public ManagerSettings() : base()
    {
        Url = SettingsManager.ReadIni(ServerSection, ServerUrlKey);
    }
}