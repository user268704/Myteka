using Myteka.Infrastructure.Data.Interfaces;
using Myteka.Models.ExternalModels.RegisterModels;
using Myteka.Models.InternalModels;
using Myteka.Models.InternalModels.Users;

namespace Myteka.Infrastructure.Data.Implementations;

public class UserRepository : IUserRepository
{
    public List<Guid> GetLikedBooks(Guid userId)
    {
        throw new NotImplementedException();
    }

    public void LikeBook(Guid userId, Guid bookId)
    {
        throw new NotImplementedException();
    }

    public void DislikeBook(Guid userId, Guid bookId)
    {
        throw new NotImplementedException();
    }

    public List<Bookmark> GetBookmarks(Guid userId, Guid bookId)
    {
        throw new NotImplementedException();
    }

    public void AddBookmark(Guid userId, Guid bookId, Bookmark bookmark)
    {
        throw new NotImplementedException();
    }

    public void RemoveBookmark(Guid userId, Guid bookId, Guid bookmarkId)
    {
        throw new NotImplementedException();
    }

    public void AddReadBook(Guid userId, ReadableBook book)
    {
        throw new NotImplementedException();
    }

    public List<ReadableBook> GetReadBooks(Guid userId)
    {
        throw new NotImplementedException();
    }

    public void RemoveReadBook(Guid userId, Guid bookId)
    {
        throw new NotImplementedException();
    }
}