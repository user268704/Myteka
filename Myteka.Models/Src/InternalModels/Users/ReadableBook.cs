namespace Myteka.Models.InternalModels.Users;

public class ReadableBook
{
    public Guid Id { get; set; }
    public Book Book { get; set; }
    public User User { get; set; }
    public int Page { get; set; }
    public ICollection<Bookmark> Bookmarks { get; set; }
}