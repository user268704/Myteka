using System.Text.Json;
using Myteka.Configuration.Models;

namespace Myteka.Configuration;

public class Config : IConfiguration
{
    private static Config Entity { get; set; }
    private ConfigModel MainModel { get; }

    private string ConfigText { get; set; }

    private Config()
    {
        Init();

        MainModel = Deserialize(ConfigText);
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
        string path = @"D:\Myteka\config.json";

        if (File.Exists(path))
        {
            string allText = File.ReadAllText(path);

            ConfigText = allText;
            
            return;
        }

        throw new FileNotFoundException("Config file not found");
    }

    private ConfigModel Deserialize(string configText)
    {
        ConfigModel config = JsonSerializer.Deserialize<ConfigModel>(configText);
        
        return config;
    }
}