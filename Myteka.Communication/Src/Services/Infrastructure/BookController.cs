using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using Myteka.Communication.Data.Models;
using Myteka.Models.ExternalModels;

namespace Myteka.Communication.Controllers.Infrastructure;

public class BookController 
{
    private readonly BookEndPoints _endPoints;
    private HttpClient _httpClient;

    public BookController(IOptions<BookEndPoints> endPoints)
    {
        _endPoints = endPoints.Value;
    }
    
    public IEnumerable<BookExternal> GetBookByTheme(string theme)
    {
        var books = _httpClient.GetStringAsync(_endPoints.GetBookByTheme).Result;
        
        return JsonSerializer.Deserialize<IEnumerable<BookExternal>>(books);
    }
    
    public BookExternal GetBookById(Guid id)
    {
        var books = _httpClient.GetStringAsync(_endPoints.GetBookById).Result;
        
        return JsonSerializer.Deserialize<BookExternal>(books);
    }

    public IEnumerable<BookExternal> GetAllBooks()
    {
        var books = _httpClient.GetStringAsync(_endPoints.GetAllBooks).Result;
        
        return JsonSerializer.Deserialize<IEnumerable<BookExternal>>(books);
    }

    public HttpStatusCode CreateBook(BookRegisterModel book)
    {
        var books = _httpClient.GetAsync(_endPoints.GetBookByTheme).Result;
        
        return books.StatusCode;
    }

    public HttpStatusCode DeleteBook(Guid id)
    {
        var books = _httpClient.GetAsync(_endPoints.GetBookByTheme).Result;
        
        return books.StatusCode;
    }
}