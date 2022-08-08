using Myteka.Models.InternalModels;

namespace Myteka.Models.ExternalModels;

public class AudioExternal
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Size { get; set; }
    public string Duration { get; set; }
    public Author Author { get; set; }
}