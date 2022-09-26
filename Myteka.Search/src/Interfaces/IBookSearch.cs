using Myteka.Models.ExternalModels;

namespace Myteka.Search.Interfaces;

public interface IBookSearch
{
    IEnumerable<BookExternal> SearchByTitle(string title);
    IEnumerable<BookExternal> SearchByDescription(string searchString);
}