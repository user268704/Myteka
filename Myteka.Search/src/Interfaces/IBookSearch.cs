using Myteka.Models.ExternalModels;
using Myteka.Models.InternalModels;

namespace Myteka.Search.Interfaces;

public interface IBookSearch
{
    IEnumerable<Book> SearchByTitle(string title);
    IEnumerable<Book> SearchByDescription(string searchString);
    IEnumerable<Book> SearchByTags(string[] tags);
}