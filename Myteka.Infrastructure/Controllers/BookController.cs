using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Myteka.Infrastructure.Data.Interfaces;
using Myteka.Infrastructure.Validators;
using Myteka.Models;
using Myteka.Models.ExternalModels;
using Myteka.Models.InternalModels;

namespace Myteka.Infrastructure.Controllers;

[Produces("application/json")]
public class BookController : BaseController
{
    IBookRepository _bookRepository;
    private readonly IMapper _mapper;
    private readonly IAuthorRepository _authorRepository;

    public BookController(IBookRepository bookRepository, 
        IMapper mapper,
        IAuthorRepository authorRepository)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
        _authorRepository = authorRepository;
    }

    /// <summary>
    /// Returns books by topic
    /// </summary>
    /// <param name="theme">Theme</param>
    /// <returns>A list of books suitable for the request</returns>
    /// <response code="200">Returns a list of books</response>
    /// <response code="403">If the request is invalid</response>
    [Route("get/bytheme/")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetBookByTheme(string theme)
    {
        if (string.IsNullOrWhiteSpace(theme))
            return BadRequest();

        ICollection<Book> books = _bookRepository.GetAll();
        var result = books.Where(b => b.Theme == theme);
        
        IEnumerable<BookExternal> mappedResult = _mapper.Map<IEnumerable<BookExternal>>(result);
        return Ok(mappedResult);
    }
    
    /// <summary>
    /// Returns the book by id
    /// </summary>
    /// <param name="id">Book id</param>
    /// <returns>The book with the requested id</returns>
    /// <response code="200">Returns the book</response>
    /// <response code="404">There is no such book</response>
    [Route("get/byid/")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetBookById(Guid id)
    {
        var book = _bookRepository.GetById(id);
        if (book == null)
            return NotFound(new ErrorResponse
            {
                Error = "There is no such book",
                ErrorCode = 404
            });
        
        var mappedBook = _mapper.Map<BookExternal>(book);
        return Ok(mappedBook);
    }
    
    /// <summary>
    /// Returns all books
    /// </summary>
    /// <response code="200">Returns a list of books</response>
    [Route("get/all")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAllBooks()
    {
        ICollection<Book> books = _bookRepository.GetAll();
        var mappedBooks = _mapper.Map<IEnumerable<BookExternal>>(books);
        
        return Ok(mappedBooks);
    }

    /// <summary>
    /// Creates a book and writes it to the database
    /// </summary>
    /// <param name="book">The book being created</param>
    /// <returns></returns>
    /// <response code="200">The book has been added successfully</response>
    /// <response code="400">If the request is invalid</response>
    [Route("create")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreateBook(BookRegisterModel book)
    {
        Book bookToCreate = _mapper.Map<Book>(book);
        
        var bookValidator = new BookValidator();
        var validationResult = bookValidator.Validate(bookToCreate);

        if (_bookRepository.CheckForExists(book))
            return BadRequest(new ErrorResponse
            {
                Error = "Book already exists",
                ErrorCode = 400
            });
        if (!_authorRepository.Contains(book.AuthorId))
            return BadRequest(new ErrorResponse
            {
                Error = "There is no such author",
                ErrorCode = 400
            });

        if (validationResult.IsValid)
        {
            _bookRepository.Add(bookToCreate);
            _bookRepository.SaveChanges();
            return Ok();
        }
        
        return BadRequest(validationResult.Errors);
    }
    
    /// <summary>
    /// Deletes a book by id
    /// </summary>
    /// <param name="id">Id of the book to be deleted</param>
    /// <response code="200">The book has been deleted successfully</response>
    /// <response code="400">There is no such book</response>
    /// <response code="403">No rights to delete</response>
    [Route("delete/{id}")]
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult DeleteBook(Guid id)
    {
        if (_bookRepository.CheckById(id))
        {
            _bookRepository.Remove(id);
            _bookRepository.SaveChanges();
        
            return Ok();
        }

        return BadRequest();
    }
}