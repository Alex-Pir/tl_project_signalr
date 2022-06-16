using System;
using System.IO;
using PmsAgentProxy.Services.Settings;

namespace PmsAgentProxy.Settings;

public abstract class BaseSettings
{
    protected readonly ClientSettingsManager SettingsManager;

    public BaseSettings()
    {
        SettingsConfigSection settingsConfig = (SettingsConfigSection)new SettingsConfigSection().GetData();
        
        string filePath = Path.GetFullPath($"{AppDomain.CurrentDomain.BaseDirectory}/{settingsConfig.Path}");
        
        SettingsManager = new  ClientSettingsManager(filePath);
    }
}