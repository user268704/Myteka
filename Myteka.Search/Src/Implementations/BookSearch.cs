using Myteka.Communication;
using Myteka.Communication.Controllers.Infrastructure;
using Myteka.Models.ExternalModels;
using Myteka.Search.Interfaces;

namespace Myteka.Search.Implementations;

public class BookSearch : IBookSearch
{
    BookController _connection;
    public BookSearch()
    {
        _connection = new InfrastructureConnection().Book;
    }
    
    public IEnumerable<BookExternal> SearchByTitle(string title)
    {
        IEnumerable<BookExternal> allBooks = _connection.GetAllBooks();
        
        var results  = allBooks.Where(book => book.Title.ToLower().Contains(title.ToLower()));
        if (results.Any())
        {
            return results;
        }
        
        return null;
    }

    public IEnumerable<BookExternal> SearchByDescription(string searchString)
    {
        // TODO: Сделать поиск
    }
}