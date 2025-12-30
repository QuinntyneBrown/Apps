using BookReadingTrackerLibrary.Api.Features.Books;
using BookReadingTrackerLibrary.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookReadingTrackerLibrary.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<BooksController> _logger;

    public BooksController(IMediator mediator, ILogger<BooksController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BookDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks(
        [FromQuery] Guid? userId,
        [FromQuery] Genre? genre,
        [FromQuery] ReadingStatus? status)
    {
        _logger.LogInformation("Getting books for user {UserId}", userId);

        var result = await _mediator.Send(new GetBooksQuery
        {
            UserId = userId,
            Genre = genre,
            Status = status,
        });

        return Ok(result);
    }

    [HttpGet("{bookId:guid}")]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BookDto>> GetBookById(Guid bookId)
    {
        _logger.LogInformation("Getting book {BookId}", bookId);

        var result = await _mediator.Send(new GetBookByIdQuery { BookId = bookId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BookDto>> CreateBook([FromBody] CreateBookCommand command)
    {
        _logger.LogInformation("Creating book for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/books/{result.BookId}", result);
    }

    [HttpPut("{bookId:guid}")]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BookDto>> UpdateBook(Guid bookId, [FromBody] UpdateBookCommand command)
    {
        if (bookId != command.BookId)
        {
            return BadRequest("Book ID mismatch");
        }

        _logger.LogInformation("Updating book {BookId}", bookId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{bookId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBook(Guid bookId)
    {
        _logger.LogInformation("Deleting book {BookId}", bookId);

        var result = await _mediator.Send(new DeleteBookCommand { BookId = bookId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
