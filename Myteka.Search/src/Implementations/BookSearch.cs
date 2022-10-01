using Myteka.Infrastructure.Data;
using Myteka.Models.InternalModels;
using Myteka.Search.Interfaces;

namespace Myteka.Search.Implementations;

public class BookSearch : IBookSearch
{
    private readonly DataContext _dataRepository;

    public BookSearch(DataContext dataContext)
    {
        _dataRepository = dataContext;
    }
    
    public IEnumerable<Book> SearchByTitle(string title)
    {
        var results = _dataRepository.Books.Where(opt => opt.Title.Contains(title, StringComparison.OrdinalIgnoreCase));

        if (results.Any())
            return results;

        return new List<Book>();
    }

    public IEnumerable<Book> SearchByDescription(string searchString)
    {
        // TODO: Сделать поиск
        return new List<Book>();
    }

    public IEnumerable<Book> SearchByTags(string[] tags)
    {
        var results =
            _dataRepository.Books.Where(opt =>
                tags.Any(tag => opt.Tags.Contains(tag, StringComparer.OrdinalIgnoreCase)));
        
        if (results.Any())
            return results;
        
        return new List<Book>();
    }
    
    private List<string> GetAllWords()
    {
        List<string> bookTitles = _dataRepository.Books.Select(book => book.Title).ToList();
        List<string> allWords = bookTitles.SelectMany(bookTitle => bookTitle.Split(' ')).ToList();

        return allWords;
    }

    private string[] NgramSplit(string word, int n)
    {
        return new string[2];
    }
}