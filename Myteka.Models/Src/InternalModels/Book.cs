using System.ComponentModel.DataAnnotations.Schema;
using Myteka.Models.ExternalModels;

namespace Myteka.Models.InternalModels;

public class Book
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    
    [ForeignKey("AuthorId")]
    public Guid AuthorId { get; set; }
    
    [ForeignKey("ContentId")]
    public Guid ContentId { get; set; }
    public DateTime WritingDate { get; set; }
    public DateTime UploadDate { get; set; }
    public string[] Tags { get; set; }
    public string Genre { get; set; }
}