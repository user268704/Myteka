using Myteka.Infrastructure.Data.Interfaces;
using Myteka.Models.ExternalModels.RegisterModels;
using Myteka.Models.InternalModels;

namespace Myteka.Infrastructure.Data.Implementations;

public class BookRepository : IBookRepository
{
    private readonly DataContext _dataContext;
    public BookRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public ICollection<Book> GetAll(int count)
    {
        if (count > 0)
            return _dataContext.Books.Take(count).ToList();

        throw new ArgumentException("Count must be greater than 0", nameof(count));
    }

    public ICollection<Book> GetAll() => 
        _dataContext.Books.ToList();

    public ICollection<Book> GetAll(Func<Book, bool> predicate) =>
        _dataContext.Books.Where(predicate) as ICollection<Book>;

    public Book GetById(Guid id) => 
        _dataContext.Books.Find(id) ?? new();

    public bool CheckById(Guid id)
    {
        return _dataContext.Books.Any(b => b.Id == id);
    }

    public void Add(Book book)
    {
        book.UploadDate = DateTime.Now;

        _dataContext.Books.Add(book);
    }

    public void Update(Book book)
    {
        throw new NotImplementedException();
    }

    public void Remove(Guid id)
    {
        Book? book = _dataContext.Books.Find(id);
        
        if (book != null)
        {
            _dataContext.Books.Remove(book);
            return;
        }
        
        throw new ArgumentException("Book with this id not found", nameof(id));
    }

    public bool DeepCheckExists(BookRegisterModel checkingBook)
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