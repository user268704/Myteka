using System.Text.Json;
using Myteka.Configuration.Models;

namespace Myteka.Configuration;

public class Config : IConfiguration
{
    
    const string ConfigFile = @"D:\Myteka\config.json";
    
    private static Config Entity { get; set; }
    private ConfigModel MainModel { get; }

    private string ConfigText { get; set; }

    private Config()
    {
        Init();

        MainModel = DeserializeJson(ConfigText);
    }

    public static Config GetConfig()
    {
        if (Entity == null)
        {
            Entity = new();

            return Entity;
        }
        
        return Entity;
    }
    
    public ConfigModel Get()
    {
        return MainModel;
    }

    private void Init()
    {
        string path = ConfigFile;

        if (File.Exists(path))
        {
            string allText = File.ReadAllText(path);

            ConfigText = allText;
            
            return;
        }

        throw new FileNotFoundException("Config file not found");
    }
    
    private ConfigModel DeserializeJson(string configText)
    {
        ConfigModel config = JsonSerializer.Deserialize<ConfigModel>(configText);
        
        return config;
    }
}