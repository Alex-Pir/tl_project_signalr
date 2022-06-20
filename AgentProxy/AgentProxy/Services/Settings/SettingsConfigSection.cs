namespace AgentProxy.Services.Settings;

public class SettingsConfigSection : ConfigSection
{
    protected override string GetParameterKey()
    {
        return "settings";
    }
}