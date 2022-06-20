using System;
using System.Configuration;

namespace AgentProxy.Services;

public abstract class ConfigSection
{
    public string Value { get; protected set; }
    
    public ConfigSection()
    {
        Value = GetConfigData();
    }
    
    private string GetConfigData()
    {
        string parameter = ConfigurationManager.AppSettings[GetParameterKey()];
        
        if (parameter == null)
        {
            throw new ArgumentException("Settings were not found. Please check settings in the file");
        }
            
        return parameter;
    }

    protected abstract string GetParameterKey();
}