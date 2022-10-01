using System.ComponentModel.DataAnnotations.Schema;

namespace Myteka.Models.InternalModels;

public class Content
{
    public Guid Id { get; set; }
    public string? Url { get; set; }
    public string? Path { get; set; }
    public string FileName { get; set; }

    [ForeignKey("MetadataId")]
    public ContentMetadata Metadata { get; set; }
}