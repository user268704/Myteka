namespace Myteka.Models.ExternalModels;

public class BookExternal
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid AuthorId { get; set; }
    public Guid ContentId { get; set; }
    public DateTime WritingDate { get; set; }
    public DateTime UploadDate { get; set; }
    public string[] Tags { get; set; }
    public string Genre { get; set; }
}