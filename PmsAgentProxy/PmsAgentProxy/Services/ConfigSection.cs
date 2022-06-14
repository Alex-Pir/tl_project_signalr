using System;
using System.Configuration;
using System.Web.Configuration;
using PmsAgentProxy.Services.RemoteServices;

namespace PmsAgentProxy.Services;

public abstract class ConfigSection : ConfigurationSection
{
    public ConfigurationSection GetData()
    {
        ConfigSection service = (ConfigSection)WebConfigurationManager.GetSection(GetGroupName());
            
        if (service == null)
        {
            throw new Exception("Service settings were not found. Please check settings in the file");
        }
            
        return service;
    }

    protected abstract string GetGroupName();
}