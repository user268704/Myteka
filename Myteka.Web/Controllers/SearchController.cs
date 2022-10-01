using Microsoft.AspNetCore.Mvc;
using Myteka.Search.Interfaces;

namespace Myteka.Web.Controllers;

public class SearchController : BaseController
{
    private readonly IBookSearch _bookSearch;
    private readonly IAuthorSearch _authorSearch;
    private readonly IContentSearch _contentSearch;

    public SearchController(IBookSearch bookSearch, 
        IAuthorSearch authorSearch,
        IContentSearch contentSearch)
    {
        _bookSearch = bookSearch;
        _authorSearch = authorSearch;
        _contentSearch = contentSearch;
    }

    #region Books

    /// <summary>
    /// Book search by title
    /// </summary>
    /// <param name="title">The name of the book you are looking for</param>
    /// <returns>Search results</returns>
    /// <response code="200">Returns the search results</response>
    /// <response code="400"/>Not found</response>
    [Route("title")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult SearchByTitle(string title)
    {
        try
        {
            var books = _bookSearch.SearchByTitle(title);
            
            if (books.Any())
            {
                return Ok(books);
            }
        }
        catch (HttpRequestException e)
        {
            return StatusCode(500, e.Message);
        }
        
        
        return NotFound();
    }
    
    /// <summary>
    /// Book search by author name
    /// </summary>
    /// <param name="author">Author name</param>
    /// <returns>Returns the search results</returns>
    [Route("author")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult SearchByAuthor(string author)
    { 
        return Ok();
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
        return Ok();
    }
    
    /// <summary>
    /// Book search by tags
    /// </summary>
    /// <param name="tags"></param>
    /// <returns></returns>
    [Route("tags")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult SearchByTags(string[] tags)
    { 
        return Ok();
    }

    #endregion
}