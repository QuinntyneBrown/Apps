// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnniversaryBirthdayReminder.Api;

/// <summary>
/// API controller for managing gifts.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class GiftsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<GiftsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GiftsController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public GiftsController(IMediator mediator, ILogger<GiftsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all gifts for an important date.
    /// </summary>
    /// <param name="dateId">The important date ID.</param>
    /// <returns>The list of gifts.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GiftDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<GiftDto>>> GetByDateId([FromQuery] Guid dateId)
    {
        _logger.LogInformation("Getting gifts for important date {ImportantDateId}", dateId);

        var result = await _mediator.Send(new GetGiftsByDateIdQuery { ImportantDateId = dateId });

        return Ok(result);
    }

    /// <summary>
    /// Adds a new gift idea.
    /// </summary>
    /// <param name="command">The add gift idea command.</param>
    /// <returns>The created gift.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(GiftDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GiftDto>> AddIdea([FromBody] AddGiftIdeaCommand command)
    {
        _logger.LogInformation(
            "Adding gift idea for important date {ImportantDateId}",
            command.ImportantDateId);

        var result = await _mediator.Send(command);

        return Created(string.Empty, result);
    }

    /// <summary>
    /// Marks a gift as purchased.
    /// </summary>
    /// <param name="id">The gift ID.</param>
    /// <param name="command">The mark purchased command.</param>
    /// <returns>The updated gift.</returns>
    [HttpPost("{id:guid}/purchase")]
    [ProducesResponseType(typeof(GiftDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GiftDto>> MarkPurchased(Guid id, [FromBody] MarkGiftPurchasedCommand command)
    {
        if (id != command.GiftId)
        {
            return BadRequest("ID mismatch");
        }

        _logger.LogInformation("Marking gift {GiftId} as purchased", id);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Marks a gift as delivered.
    /// </summary>
    /// <param name="id">The gift ID.</param>
    /// <returns>The updated gift.</returns>
    [HttpPost("{id:guid}/deliver")]
    [ProducesResponseType(typeof(GiftDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GiftDto>> MarkDelivered(Guid id)
    {
        _logger.LogInformation("Marking gift {GiftId} as delivered", id);

        var result = await _mediator.Send(new MarkGiftDeliveredCommand { GiftId = id });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
