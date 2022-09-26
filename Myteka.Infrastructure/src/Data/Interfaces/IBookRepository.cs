using Myteka.Models.ExternalModels;
using Myteka.Models.ExternalModels.RegisterModels;
using Myteka.Models.InternalModels;

namespace Myteka.Infrastructure.Data.Interfaces;

public interface IBookRepository
{
    ICollection<Book> GetAll(int count);
    ICollection<Book> GetAll();
    Book GetById(Guid id);
    bool CheckById(Guid id);
    void Add(Book book);
    void Update(Book book);
    void Remove(Guid id);
    bool CheckForExists(BookRegisterModel checkingBook);
    void SaveChanges();
}