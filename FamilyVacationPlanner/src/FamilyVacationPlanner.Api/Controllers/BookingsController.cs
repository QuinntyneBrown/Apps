using FamilyVacationPlanner.Api.Features.Bookings;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FamilyVacationPlanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<BookingsController> _logger;

    public BookingsController(IMediator mediator, ILogger<BookingsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BookingDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BookingDto>>> GetBookings([FromQuery] Guid? tripId)
    {
        _logger.LogInformation("Getting bookings for trip {TripId}", tripId);

        var result = await _mediator.Send(new GetBookingsQuery { TripId = tripId });

        return Ok(result);
    }

    [HttpGet("{bookingId:guid}")]
    [ProducesResponseType(typeof(BookingDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BookingDto>> GetBookingById(Guid bookingId)
    {
        _logger.LogInformation("Getting booking {BookingId}", bookingId);

        var result = await _mediator.Send(new GetBookingByIdQuery { BookingId = bookingId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(BookingDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BookingDto>> CreateBooking([FromBody] CreateBookingCommand command)
    {
        _logger.LogInformation("Creating booking for trip {TripId}", command.TripId);

        var result = await _mediator.Send(command);

        return Created($"/api/bookings/{result.BookingId}", result);
    }

    [HttpPut("{bookingId:guid}")]
    [ProducesResponseType(typeof(BookingDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BookingDto>> UpdateBooking(Guid bookingId, [FromBody] UpdateBookingCommand command)
    {
        if (bookingId != command.BookingId)
        {
            return BadRequest("Booking ID mismatch");
        }

        _logger.LogInformation("Updating booking {BookingId}", bookingId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{bookingId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBooking(Guid bookingId)
    {
        _logger.LogInformation("Deleting booking {BookingId}", bookingId);

        var result = await _mediator.Send(new DeleteBookingCommand { BookingId = bookingId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
