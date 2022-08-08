using Microsoft.AspNetCore.Mvc;

namespace Myteka.Search.Controllers;

[Route("api/book/search")]
public class BookSearchController : BaseController
{
    /// <summary>
    /// Book search by title
    /// </summary>
    /// <param name="title">The name of the book you are looking for</param>
    /// <returns>Search results</returns>
    /// <response code="200">Returns the search results</response>
    [Route("title")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult SearchByTitle(string title)
    {
        return Ok();
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
}