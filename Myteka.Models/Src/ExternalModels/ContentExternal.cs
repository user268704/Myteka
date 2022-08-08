using Myteka.Models.InternalModels;

namespace Myteka.Models.ExternalModels;

public class ContentExternal
{
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public ContentMetadata Metadata { get; set; }
    public Guid BookId { get; set; }
}