namespace Myteka.Models.InternalModels.Users;

public class Bookmark
{
    public Guid Id { get; set; }
    public User User { get; set; }
    public Book Book { get; set; }
    public int Page { get; set; }
    public int LineNumber { get; set; }
    public string Message { get; set; }
}