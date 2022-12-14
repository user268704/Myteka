using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Myteka.Infrastructure.Data.Interfaces;
using Myteka.Infrastructure.Exceptions;
using Myteka.Models.InternalModels;

namespace Myteka.Infrastructure.Controllers;

public class ContentController : BaseController
{
    private readonly IContentRepository _contentRepository;
    private readonly IMapper _mapper;
    private readonly IBookRepository _bookRepository;

    public ContentController(IContentRepository contentRepository, 
        IMapper mapper,
        IBookRepository bookRepository)
    {
        _contentRepository = contentRepository;
        _mapper = mapper;
        _bookRepository = bookRepository;
    }

    [Route("download/{fileId}")]
    [HttpGet]
    [Produces("application/octet-stream")]
    public async Task<IActionResult> DownloadFile(Guid fileId)
    {
        try
        {
            byte[] file = _contentRepository.DownloadFile(fileId);

            Content content =  await _contentRepository.GetContentAsync(fileId);
            
            return File(file, "application/octet-stream", content.FileName);
        }
        catch (GuidNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Upload book to server
    /// </summary>
    /// <param name="file"></param>
    /// <returns>Returns the id of the book that has just been downloaded</returns>
    /// <response code="200">Book uploaded successfully</response>
    /// <response code="400">Book already exists</response>
    [Route("upload/book")]
    [HttpPost]
    public IActionResult UploadBook(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("File is empty"); ;
        
        Stream fileStream = file.OpenReadStream();
        byte[] fileBytes = new byte[file.Length];
        fileStream.Read(fileBytes, 0, (int)file.Length);

        try
        {
            Guid fileId = _contentRepository.UploadFile(fileBytes, file.FileName);
            return Ok(new { FileId = fileId });
        }
        catch (FileAlreadyExistsException)
        {
            return BadRequest(new { Error = "Book already exists" });
        }

    }

    /// <summary>
    /// Bind book to content metadata
    /// </summary>
    /// <param name="bookId"></param>
    /// <param name="fileId"></param>
    /// <returns></returns>
    [Route("upload/book/bind")]
    [HttpPost]
    public IActionResult BindBook(Guid fileId, Guid bookId)
    {
        _contentRepository.BindContent(fileId, bookId);

        return Ok();
    }
    
    /// <summary>
    /// Remove book from server
    /// </summary>
    /// <param name="fileId"></param>
    /// <returns></returns>
    [HttpDelete]
    public IActionResult RemoveBook(Guid fileId)
    {
        try
        {
            _contentRepository.RemoveContent(fileId);

            return Ok();
        }
        catch (GuidNotFoundException)
        {
            return NotFound();
        }
    }
    
    [Route("get/all")]
    [HttpGet]
    public IEnumerable<Content> GetAllContent(int? count)
    {
        if (count == null)
        {
            return _contentRepository.GetAll();
        }

        return _contentRepository.GetAll((int)count);
    }
}