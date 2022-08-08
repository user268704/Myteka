using Myteka.Models.InternalModels;

namespace Myteka.Infrastructure.Data.Interfaces;

public interface IContentRepository
{
    byte[] DownloadFile(Guid fileId);
    string GetContentUrl(Guid contentId);
    Guid UploadFile(byte[] file, string fileName);
    void ChangeContent(Guid oldContentId, Content newContent, byte[] contentFile);
    void DeleteContent(Guid contentId);
    Content GetContent(Guid contentId);
    void BindContent(Guid fileId, Guid bookId);
    ContentMetadata GetMetadata(Guid contentId);
    void UpdateMetadata(Guid contentId, ContentMetadata newMetadata);
    bool CheckById(Guid contentId);
}