using System.Text.Json.Serialization;

namespace Myteka.Configuration.Models;

public class Global
{
    public string FileSavePath { get; set; }
    
    [JsonPropertyName("AllowedExtensionsPath")]
    public string AllowedExtensions { get; set; }
}