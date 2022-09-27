using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Myteka.Exceptions;
using Myteka.Infrastructure.Data.Interfaces;
using Myteka.Models;
using Myteka.Models.ExternalModels;
using Myteka.Models.ExternalModels.RegisterModels;
using Myteka.Models.InternalModels;

namespace Myteka.Web.Controllers;

public class AuthorController : BaseController
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;

    public AuthorController(IAuthorRepository authorRepository, IMapper mapper)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Returns all the author's books
    /// </summary>
    /// <param name="authorId">Author id</param>
    /// <returns>All books by the author</returns>
    /// <response code="200">Returns all the books by the author</response>
    /// <response code="404">Author not found</response>
    [Route("get/books/{authorId}")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetAllAuthorBooks(Guid authorId)
    {
        ICollection<Book> books = _authorRepository.GetBooks(authorId);
        if (books.Any())
        {
            // map the books to the external model
            var booksResult = _mapper.Map<IEnumerable<BookExternal>>(books);
            return Ok(booksResult);
        }

        return NotFound();
    }
    
    /// <summary>
    /// Returns all authors
    /// </summary>
    /// <returns>All authors</returns>
    /// <response code="200">Returns all authors</response>
    [Route("get/all")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAllAuthors()
    {
        var authors = _authorRepository.GetAuthors();
        var authorsResult = _mapper.Map<IEnumerable<AuthorExternal>>(authors);
        
        return Ok(authorsResult);
    }

    /// <summary>
    /// Returns all authors but with a limited number 
    /// </summary>
    /// <returns>Authors</returns>
    /// <response code="200">Returns all authors</response>
    /// <response code="400">Invalid request</response>
    [Route("get/all/{count}")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetAllAuthors(int count)
    {
        if (count <= 0)
            return BadRequest();
        
        var authors = _authorRepository.GetAuthors(count);
        var authorsResult = _mapper.Map<IEnumerable<AuthorExternal>>(authors);
        
        return Ok(authorsResult);
    }
    
    
    /// <summary>
    /// Adds a new book to the author's book list
    /// </summary>
    /// <param name="authorId">author's id</param>
    /// <param name="bookId">book's id</param>
    /// <response code="200">Book added to the author's book list</response>
    /// <response code="404">Author not found</response>
    /// <response code="404">Book not found</response>
    [Route("add/book/")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult AddBook(Guid authorId, Guid bookId)
    {
        try
        {
            _authorRepository.AddBook(authorId, bookId);
            _authorRepository.SaveChanges();
            return Ok();
        }
        catch (NotFoundException e)
        {
            return NotFound(new ErrorResponse
            {
                Error = e.Message,
                ErrorCode = 404
            });
        }
    }

    /// <summary>
    /// Returns the author by his id
    /// </summary>
    /// <param name="authorId">author's id</param>
    /// <returns>Requested author</returns>
    /// <response code="200">Returns the author by his id</response>
    /// <response code="404">Author not found</response>
    [Route("get/{authorId}")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Get(Guid authorId)
    {
        Author? author = _authorRepository.GetAuthor(authorId);

        if (author == null)
            return NotFound();

        var authorResult = _mapper.Map<AuthorExternal>(author);
        
        return Ok(authorResult);
    }

    /// <summary>
    /// Creates a new author and saves it in the database
    /// </summary>
    /// <param name="author">New author</param>
    /// <response code="200">Author created</response>
    /// <response code="400">Invalid request</response>
    [Route("add")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create(AuthorRegisterModel author)
    {
        try
        {
            var authorOriginal = _mapper.Map<Author>(author);
            
            _authorRepository.Add(authorOriginal);
            _authorRepository.SaveChanges();
            return Ok();
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }
    }

    /// <summary>
    /// Returns the author's tags
    /// </summary>
    /// <param name="authorId">author's id</param>
    /// <returns>Tags assigned to the author</returns>
    /// <response code="200">Returns the author's tags</response>
    /// <response code="404">Author not found</response>
    [Route("get/tags/{authorId}")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetAuthorTags(Guid authorId)
    {
        var tags = _authorRepository.GetAuthorTags(authorId);
        if (tags.Any())
            return Ok(tags);
        
        return NotFound();
    }
    
    /// <summary>
    /// Removes a specific author from the database
    /// </summary>
    /// <param name="authorId">author's id</param>
    /// <response code="200">Author removed</response>
    /// <response code="404">Author not found</response>
    /// <response code="403">No rights to delete</response>
    [Route("remove/{authorId}")]
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult Remove(Guid authorId)
    {
        bool isAuthorExists = _authorRepository.CheckById(authorId);
        
        if (isAuthorExists)
        {
            _authorRepository.Remove(authorId);
            _authorRepository.SaveChanges();
            return Ok();
        }

        return NotFound();
    }
}