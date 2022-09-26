using System.Text.Json;
using Myteka.Configuration;
using Myteka.Configuration.Models;
using Myteka.Models.ExternalModels;
using Myteka.Search.Interfaces;
using IConfiguration = Myteka.Configuration.IConfiguration;

namespace Myteka.Search.Implementations;

public class BookSearch : IBookSearch
{
    HttpClient httpClient = new();
    IConfiguration configuration;
    private ConfigModel ConfigModel { get; }

    public BookSearch()
    {
        
        configuration = Config.GetConfig();
        
        ConfigModel = configuration.Get();
    }
    
    public IEnumerable<BookExternal> SearchByTitle(string title)
    {
        string url = ConfigModel.Urls.Infrastructure.Split(';')[0] + "/api/" + ConfigModel.EndPoints.Infrastructure.Book.GetAllBooks;

        var response = httpClient
            .GetStringAsync(url)
            .Result;


        List<BookExternal> allBooks = JsonSerializer.Deserialize<List<BookExternal>>(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
        
        var results  = allBooks.Where(book => book.Title.ToLower().Contains(title.ToLower()));
        if (results.Any())
        {
            return results;
        }
        
        return new List<BookExternal>();
    }

    public IEnumerable<BookExternal> SearchByDescription(string searchString)
    {
        return new List<BookExternal>();
        // TODO: Сделать поиск
    }
}