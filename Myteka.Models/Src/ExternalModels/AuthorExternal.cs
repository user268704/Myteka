using Myteka.Models.InternalModels;

namespace Myteka.Models.ExternalModels;

public class AuthorExternal
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string[] Tags { get; set; }
}