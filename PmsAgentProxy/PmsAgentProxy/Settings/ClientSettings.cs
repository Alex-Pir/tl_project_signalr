using System;
using System.IO;
using PmsAgentProxy.Services;
using PmsAgentProxy.Services.Settings;

namespace PmsAgentProxy.Settings;

public class ClientSettings
{
    private const string ServerSection = "manager";
    private const string ServerUrlKey = "url";
    public string Url { get; }

    public ClientSettings()
    {
        SettingsConfigSection settingsConfig = (SettingsConfigSection)new SettingsConfigSection().GetData();
        string filePath = Path.GetFullPath($"{AppDomain.CurrentDomain.BaseDirectory}/{settingsConfig.Path}");
        ClientSettingsManager settingsManager = new ClientSettingsManager(filePath);
        Url = settingsManager.ReadIni(ServerSection, ServerUrlKey);
    }
}