using Myteka.Models.InternalModels;

namespace Myteka.Models.ExternalModels.RegisterModels;

public class BookRegisterModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid AuthorId { get; set; }
    public Guid ContentId { get; set; }
    public string[] Tags { get; set; }
    public string Theme { get; set; }
}