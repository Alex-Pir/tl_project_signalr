using System;
using System.Configuration;
using System.Web.Configuration;
using PmsAgentProxy.Services.GuidServices;

namespace PmsAgentProxy.Services.Client;

public class ClientConfigSection : ConfigSection
{
    [ConfigurationProperty("page")] 
    public string Page => (string)this["page"];

    protected override string GetGroupName()
    {
        return "client";
    }
}