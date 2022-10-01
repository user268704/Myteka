using Myteka.Infrastructure.Data;
using Myteka.Infrastructure.Data.Implementations;
using Myteka.Models.InternalModels;
using Myteka.Search.Implementations;

namespace Myteka.Tests;

public class SearchTests
{
    [Fact]
    public void BookSearchTest()
    {
        DataContext dataContext = new DataContext();
        BookSearch bookSearch = new BookSearch(dataContext);
        
        var searchByTitle1 = bookSearch.SearchByTitle("академии");
        var searchByTitle2 = bookSearch.SearchByTitle("человек");
        var searchByTitle2WithError = bookSearch.SearchByTitle("человвк");
        
        
        var searchByTitle3 = bookSearch.SearchByTitle("как");
        
        Assert.True(searchByTitle1.Any());
        Assert.True(searchByTitle2.Any());
        Assert.True(searchByTitle3.Any());
    }
}