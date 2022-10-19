using Myteka.Models.ExternalModels.RegisterModels;
using Myteka.Models.InternalModels;
using Myteka.Models.InternalModels.Users;

namespace Myteka.Infrastructure.Data.Interfaces;

public interface IUserRepository
{
    List<Guid> GetLikedBooks(Guid userId);
    void LikeBook(Guid userId, Guid bookId);
    void DislikeBook(Guid userId, Guid bookId);
    List<Bookmark> GetBookmarks(Guid userId, Guid bookId);
    void AddBookmark(Guid userId, Guid bookId, Bookmark bookmark);
    void RemoveBookmark(Guid userId, Guid bookId, Guid bookmarkId);
    void AddReadBook(Guid userId, ReadableBook book);
    List<ReadableBook> GetReadBooks(Guid userId);
    void RemoveReadBook(Guid userId, Guid bookId);
}