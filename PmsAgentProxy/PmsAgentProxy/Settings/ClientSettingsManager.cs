using System;
using IniParser;
using IniParser.Model;

namespace PmsAgentProxy.Settings;

public class ClientSettingsManager
{
    private readonly string _settingsFilePath;
    
    public ClientSettingsManager(string settingsFilePath)
    {
        _settingsFilePath = settingsFilePath;
    }

    public string ReadIni(string section, string key)
    {
        var parser = new FileIniDataParser();
        IniData data = parser.ReadFile(_settingsFilePath);

        if (data[section][key] == null)
        {
            throw new Exception("Can not find the data in the settings file");
        }
        
        return data[section][key];
    }
}