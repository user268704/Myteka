using Myteka.Models.InternalModels;

namespace Myteka.Infrastructure.Data.Interfaces;

public interface IContentRepository
{
    byte[] DownloadFile(Guid fileId);
    string GetContentUrl(Guid contentId);
    Guid UploadFile(byte[] file, string fileName);
    void ChangeContent(Guid oldContentId, Content newContent, byte[] contentFile);
    void RemoveContent(Guid contentId);
    Task<Content> GetContentAsync(Guid contentId);
    void BindContent(Guid fileId, Guid bookId);
    ContentMetadata GetMetadata(Guid contentId);
    void UpdateMetadata(Guid contentId, ContentMetadata newMetadata);
    bool CheckById(Guid contentId);
    Content Get(Guid contentId);
    IEnumerable<Content> GetAll();
    IEnumerable<Content> GetAll(Func<Content, bool> predicate);
    IEnumerable<Content> GetAll(int count);
}