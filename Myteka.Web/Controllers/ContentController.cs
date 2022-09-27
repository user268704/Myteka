using Microsoft.AspNetCore.Mvc;
using Myteka.Exceptions;
using Myteka.Infrastructure.Data.Interfaces;
using Myteka.Models;
using Myteka.Models.InternalModels;

namespace Myteka.Web.Controllers;

public class ContentController : BaseController
{
    private readonly IContentRepository _contentRepository;

    public ContentController(IContentRepository contentRepository)
    {
        _contentRepository = contentRepository;
    }

    /// <summary>
    /// Downloads a file from the server
    /// </summary>
    /// <param name="fileId">File id</param>
    [Route("download/{fileId}")]
    [HttpGet]
    [Produces("application/octet-stream")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
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
            return NotFound(new ErrorResponse
            {
                Error = "File not found",
                ErrorCode = StatusCodes.Status404NotFound
            });
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
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UploadBook(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest(new ErrorResponse
            {
                Error = "File is empty",
                ErrorCode = StatusCodes.Status400BadRequest
            });
        
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
            return BadRequest(new ErrorResponse
            {
                Error = "File already exists",
                ErrorCode = StatusCodes.Status400BadRequest
            });
        }

    }

    /// <summary>
    /// Bind book to content metadata
    /// </summary>
    /// <param name="bookId">Book id</param>
    /// <param name="fileId">File id</param>
    /// <response code="200">Bind is successful</response>
    /// <response code="400">File or book undefined</response>
    [Route("upload/book/bind")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult BindBook(Guid fileId, Guid bookId)
    {
        try
        {
            _contentRepository.BindContent(fileId, bookId);
            
            return Ok();
        }
        catch (GuidNotFoundException e)
        {
            return BadRequest(new ErrorResponse
            {
                Error = e.Message,
                ErrorCode = StatusCodes.Status400BadRequest
            });
        }
    }
    
    /// <summary>
    /// Remove book from server
    /// </summary>
    /// <param name="fileId">File id</param>
    [Route("remove/book")]
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult RemoveBook(Guid fileId)
    {
        try
        {
            _contentRepository.RemoveContent(fileId);

            return Ok();
        }
        catch (GuidNotFoundException)
        {
            return NotFound(new ErrorResponse
            {
                Error = "Book file not found",
                ErrorCode = StatusCodes.Status404NotFound
            });
        }
    }
    
    /// <summary>
    /// Returns all content
    /// </summary>
    /// <param name="count">Number of records (Optional)</param>
    /// <response code="200">Return content</response>
    [Route("get/all")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IEnumerable<Content> GetAllContent(int? count)
    {
        if (count == null)
        {
            return _contentRepository.GetAll();
        }

        return _contentRepository.GetAll((int)count);
    }
    
    /// <summary>
    /// Return content by id
    /// </summary>
    /// <param name="contentId">Content id</param>
    /// <returns></returns>
    [Route("get/{contentId}")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetContent(Guid contentId)
    {
        try
        {
            Content content = _contentRepository.Get(contentId);
            return Ok(content);
        }
        catch (GuidNotFoundException)
        {
            return NotFound(new ErrorResponse
            {
                Error = "Content not found",
                ErrorCode = StatusCodes.Status404NotFound
            });
        }
    }

    /// <summary>
    /// Return metadata
    /// </summary>
    /// <param name="contentId">Id of the content to get metadata</param>
    [Route("get/meta/{contentId}")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetMetadata(Guid contentId)
    {
        try
        {
            ContentMetadata metadata = _contentRepository.GetMetadata(contentId);

            return Ok(metadata);
        }
        catch (GuidNotFoundException e)
        {
            return NotFound(new ErrorResponse
            {
                Error = e.Message,
                ErrorCode = StatusCodes.Status404NotFound
            });
        }
    }
}