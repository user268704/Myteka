using System.ComponentModel.DataAnnotations;
using Myteka.Exceptions;
using Myteka.Infrastructure.Data.Interfaces;
using Myteka.Models.InternalModels;

namespace Myteka.Infrastructure.Data.Implementations;

public class AuthorRepository : IAuthorRepository
{
        private DataContext _data;
    
    public AuthorRepository(DataContext data)
    {
        _data = data;
    }

    public ICollection<Author> GetAuthors(int count) => 
        _data.Authors.Take(count).ToList();

    public ICollection<Author> GetAuthors() => 
        _data.Authors.ToList();

    public ICollection<Book> GetBooks(Guid authorId)
    {
        ICollection<Book> books = _data.Books.Where(b => b.AuthorId == authorId).ToList();
        return books;
    }

    public ICollection<string> GetAuthorTags(Guid authorId)
    {
        Author? author = _data.Authors.Find(authorId);

        if (author == null)
            return new List<string>();

        return author.Tags;
    }

    public Author? GetAuthor(Guid id)
    {
        if (CheckById(id))
            return _data.Authors.Find(id);

        return null;
    }
    
    public bool CheckById(Guid id)
    {
        return _data.Authors.Any(author => author.Id == id);
    }

    public void Add(Author author)
    {
        author.Id = Guid.NewGuid();
        _data.Authors.Add(author);
    }

    /// <summary>
    /// Adds a book to the author's book list
    /// </summary>
    /// <exception cref="NotFoundException">Occurs if the book or author is not found</exception>
    public void AddBook(Guid authorId, Guid bookId)
    {
        Author? author = _data.Authors.Find(authorId);
        Book? book = _data.Books.Find(bookId);
        
        if (author == null || book == null)
            throw new NotFoundException("Author or book not found");
        
        author.Books.Add(book);
    }

    public void Remove(Guid id)
    {
        Author? author = _data.Authors.Find(id);
        if (author == null)
            throw new NotFoundException("Author not found");
        
        _data.Authors.Remove(author);
    }
    
    public void SaveChanges()
    {
        _data.SaveChanges();
    }

}