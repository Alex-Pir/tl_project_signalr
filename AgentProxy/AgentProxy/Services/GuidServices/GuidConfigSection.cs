using AgentProxy.Services;

namespace AgentProxy.Services.GuidServices;

public class GuidConfigSection : ConfigSection
{
    protected override string GetParameterKey()
    {
        return "guid";
    }
}
