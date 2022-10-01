using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Myteka.Models;
using Myteka.Models.ExternalModels;
using Myteka.Search.Interfaces;

namespace Myteka.Web.Controllers;

[Route("api/search/books")]
public class SearchBooksController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IBookSearch _bookSearch;

    public SearchBooksController(IMapper mapper, IBookSearch bookSearch)
    {
        _mapper = mapper;
        _bookSearch = bookSearch;
    }

    /// <summary>
    /// Book search by title
    /// </summary>
    /// <param name="title">The name of the book you are looking for</param>
    /// <returns>Search results</returns>
    /// <response code="200">Returns the search results</response>
    /// <response code="400">Not found</response>
    [Route("title")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult SearchByTitle(string title)
    {
        var books = _bookSearch.SearchByTitle(title);
            
        if (books.Any())
        {
            var resultBooks = _mapper.Map<IEnumerable<BookExternal>>(books);
                
            return Ok(resultBooks);
        }


        return NotFound(new ErrorResponse
        {
            Error = "Not found",
            ErrorCode = StatusCodes.Status404NotFound
        });
    }
    
    
    /// <summary>
    /// Book search by description
    /// </summary>
    /// <param name="description">Book description</param>
    /// <returns>Returns the search results</returns>
    [Route("description")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult SearchByDescription(string description)
    { 
        var books = _bookSearch.SearchByDescription(description);
            
        if (books.Any())
        {
            var resultBooks = _mapper.Map<IEnumerable<BookExternal>>(books);
                
            return Ok(resultBooks);
        }


        return NotFound(new ErrorResponse
        {
            Error = "Not found",
            ErrorCode = StatusCodes.Status404NotFound
        });
    }
    
    /// <summary>
    /// Book search by tags
    /// </summary>
    /// <param name="tags"></param>
    /// <returns></returns>
    [Route("tags")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult SearchByTags(string[] tags)
    { 
        var books = _bookSearch.SearchByTags(tags);
            
        if (books.Any())
        {
            var resultBooks = _mapper.Map<IEnumerable<BookExternal>>(books);
                
            return Ok(resultBooks);
        }


        return NotFound(new ErrorResponse
        {
            Error = "Not found",
            ErrorCode = StatusCodes.Status404NotFound
        });
    }
}