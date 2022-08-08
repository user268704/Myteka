using Myteka.Models.InternalModels;

namespace Myteka.Infrastructure.Data.Interfaces;

public interface IAuthorRepository
{
    ICollection<Author> GetAuthors(int count);
    ICollection<Author> GetAuthors();
    ICollection<Book> GetBooks(Guid authorId);
    ICollection<string> GetAuthorTags(Guid authorId);
    Author GetAuthor(Guid id);
    bool Contains(Guid id);
    void Add(Author author);
    void AddBook(Guid authorId, Guid bookId);
    void Remove(Guid id);
    void SaveChanges();
}