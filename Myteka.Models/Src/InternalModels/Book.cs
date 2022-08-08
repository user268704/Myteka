using Myteka.Models.ExternalModels;

namespace Myteka.Models.InternalModels;

public class Book
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid AuthorId { get; set; }
    public Guid ContentId { get; set; }
    public string[] Tags { get; set; }
    public string Theme { get; set; }
}