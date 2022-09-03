using System.Text.Json;
using Myteka.Configuration.Models;

namespace Myteka.Configuration;

public class Configuration : IConfiguration
{
    private string ConfigText { get; set; }

    public ConfigModel GetConfig()
    {
        Init();

        return Deserialize(ConfigText);
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