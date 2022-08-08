namespace Myteka.Models.InternalModels;

public class Author
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string[] Tags { get; set; }
    public ICollection<Book> Books { get; set; }
}