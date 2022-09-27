using Microsoft.EntityFrameworkCore;
using Myteka.Exceptions;
using Myteka.FileRepository;
using Myteka.Infrastructure.Data.Interfaces;
using Myteka.Models.InternalModels;

namespace Myteka.Infrastructure.Data.Implementations;

public class ContentRepository : IContentRepository
{
    private readonly DataContext _dataContext;
    public ContentRepository()
    {
        _dataContext = new DataContext();
    }
    
    /// <summary>
    /// Returns an object as an array of bytes
    /// </summary>
    /// <param name="fileId">id of the object being searched for</param>
    /// <exception cref="Exception">Occurs if the object is not in the database</exception>
    /// <exception cref="GuidNotFoundException">Occurs if the requested object does not exist</exception>
    public byte[] DownloadFile(Guid fileId)
    {
        Content? content = _dataContext.Content.Find(fileId);

        if (content != null)
        {
            byte[] fileResult = File.ReadAllBytes(content.Path);
        
            return fileResult;   
        }
        
        throw new GuidNotFoundException("Content not found");
    }
    
    /// <exception cref="Exception">Occurs if the url is not in the database</exception>
    /// <exception cref="GuidNotFoundException">Occurs if the requested object does not exist</exception>
    public string GetContentUrl(Guid contentId)
    {
        if (CheckById(contentId))
        {
            var content = _dataContext.Content.Find(contentId);
            if (content.Url != null)
                return content.Url;

            throw new Exception("Url is undefined");
        }
        
        throw new GuidNotFoundException("Content not found");
    }

    public Guid UploadFile(byte[] file, string fileName)
    {
        Content contentDesc = new()
        {
            Id = Guid.NewGuid(),
            FileName = fileName,
        };
        
        FileManager fileManager = new FileManager();
        FileMetaRecover fileMetaRecover = new FileMetaRecover();

        if (CheckByName(fileName))
            throw new FileAlreadyExistsException(fileName);

        fileManager.Save(contentDesc, file);
        var meta = fileMetaRecover.Recover(file, contentDesc);
        contentDesc.Metadata = meta;

        _dataContext.Content.Add(contentDesc);
        _dataContext.SaveChanges();

        return contentDesc.Id;
    }

    public void BindContent(Guid fileId, Guid bookId)
    {
        Content? fileModified = _dataContext.Content.Find(fileId);
        Book? book = _dataContext.Books.Find(bookId);

        if (fileModified != null && book != null)
        {
            fileModified.BookId = bookId;
            book.ContentId = fileModified.Id;
        
            _dataContext.SaveChanges();
            
            return;
        }
        
        throw new GuidNotFoundException("Content not found");
    }

    public void ChangeContent(Guid oldContentId, Content newContent, byte[] contentFile)
    {
        throw new NotImplementedException();
    }

    public void RemoveContent(Guid contentId)
    {
        Content? deletedObject = _dataContext.Content.Find(contentId);
        if (deletedObject != null)
        {
            FileManager fm = new FileManager();
            
            fm.Remove(deletedObject);
            _dataContext.Content.Remove(deletedObject);
            _dataContext.SaveChanges();
            
            return;
        }
        
        throw new GuidNotFoundException("Content not found");
    }

    public async Task<Content> GetContentAsync(Guid contentId)
    {
        if (CheckById(contentId)) 
        {
            Content content = await _dataContext.Content
                .Include(c => c.Metadata)
                .FirstOrDefaultAsync(content => content.Id == contentId);

            return content;
        }
        
        throw new GuidNotFoundException("Content not found");
    }
    
    

    /// <exception cref="GuidNotFoundException">Occurs if the requested object does not exist</exception>
    public ContentMetadata GetMetadata(Guid contentId)
    { 
        var content = _dataContext.Content.Find(contentId);
        if (content != null)
            return content.Metadata;

        throw new GuidNotFoundException("Content not found");
    }

    /// <summary>
    /// Updates the metadata of the object
    /// </summary>
    /// <exception cref="GuidNotFoundException">Occurs if the requested object does not exist</exception>
    public void UpdateMetadata(Guid contentId, ContentMetadata newMetadata)
    {
        if (CheckById(contentId) && newMetadata != null)
        {
            var content = _dataContext.Content.Find(contentId);
            content.Metadata = newMetadata;
            
            _dataContext.SaveChanges();
            return;
        }
        
        throw new GuidNotFoundException("Request not valid");
    }

    public Content Get(Guid contentId)
    {
        var content = _dataContext.Content.Find(contentId);
        if (content == null)
            throw new GuidNotFoundException("Content not found");

        return content;
    }

    private bool CheckByName(string name) => 
        _dataContext.Content.Any(content => content.FileName == name);

    public bool CheckById(Guid contentId) => 
        _dataContext.Content.Any(c => c.Id == contentId);

    public IEnumerable<Content> GetAll()
    {
        var allContent = _dataContext.Content;

        return allContent;
    }

    public IEnumerable<Content> GetAll(int count)
    {
        var content = _dataContext.Content
            .Take(count);

        return content;
    }

    public IEnumerable<Content> GetAll(Func<Content, bool> predicate)
    {
        var content = _dataContext.Content
            .Where(predicate);

        return content;
    }
}