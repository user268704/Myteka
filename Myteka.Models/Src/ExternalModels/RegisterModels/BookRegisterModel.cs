namespace Myteka.Models.ExternalModels;

public class BookRegisterModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid AuthorId { get; set; }
    public string[] Tags { get; set; }
    public string Theme { get; set; }

}