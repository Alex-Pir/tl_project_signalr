using System.Configuration;

namespace PmsAgentProxy.Services.Settings;

public class SettingsConfigSection : ConfigSection
{
    [ConfigurationProperty("path")] 
    public string Path => (string)this["path"];

    protected override string GetGroupName()
    {
        return "settings";
    }
}