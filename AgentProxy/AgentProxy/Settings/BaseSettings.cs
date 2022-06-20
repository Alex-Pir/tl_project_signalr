using System;
using System.IO;
using AgentProxy.Services.Settings;

namespace AgentProxy.Settings;

public abstract class BaseSettings
{
    protected readonly ClientSettingsManager SettingsManager;

    public BaseSettings()
    {
        string applicationSettingsFilePath = new SettingsConfigSection().Value;
        
        string filePath = Path.GetFullPath($"{AppDomain.CurrentDomain.BaseDirectory}/{applicationSettingsFilePath}");
        
        SettingsManager = new  ClientSettingsManager(filePath);
    }
}