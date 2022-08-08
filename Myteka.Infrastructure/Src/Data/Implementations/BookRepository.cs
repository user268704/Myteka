using AutoMapper;
using Myteka.Infrastructure.Data.Interfaces;
using Myteka.Models.ExternalModels;
using Myteka.Models.InternalModels;

namespace Myteka.Infrastructure.Data.Implementations;

public class BookRepository : IBookRepository
{
    private DataContext _dataContext;
    public BookRepository()
    {
        DataContext db = new DataContext();
        _dataContext = db;
    }
    
    public ICollection<Book> GetAll(int count)
    {
        return _dataContext.Books.Take(count).ToList();
    }

    public ICollection<Book> GetAll()
    {
        return _dataContext.Books.ToList();
    }

    public Book? GetById(Guid id)
    {
        Book? book = _dataContext.Books.Find(id);
        return book;
    }

    public bool CheckById(Guid id)
    {
        bool contains = _dataContext.Books.Any(b => b.Id == id);
        return contains;
    }

    public void Add(Book book)
    {
        book.Id = Guid.NewGuid();
        
        _dataContext.Books.Add(book);
    }

    public void Update(Book book)
    {
        throw new NotImplementedException();
    }

    public void Remove(Guid id)
    {
        if (CheckById(id))
        {
            _dataContext.Books.Remove(_dataContext.Books.Find(id));
        }
        else
        {
            throw new Exception("Book not found");
        }
    }

    public bool CheckForExists(BookRegisterModel checkingBook)
    {
        bool isTitleExists = _dataContext.Books.Any(book => book.Title == checkingBook.Title);
        bool isDescriptionExists = _dataContext.Books.Any(book => book.Title == checkingBook.Description);
        
        return isTitleExists || isDescriptionExists;
    }

    public void SaveChanges()
    {
        _dataContext.SaveChanges();
    }
}