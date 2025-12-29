// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnniversaryBirthdayReminder.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AnniversaryBirthdayReminder.Api;

/// <summary>
/// Command to add a gift idea.
/// </summary>
public record AddGiftIdeaCommand : IRequest<GiftDto>
{
    /// <summary>
    /// Gets or sets the important date ID.
    /// </summary>
    public Guid ImportantDateId { get; init; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the estimated price.
    /// </summary>
    public decimal EstimatedPrice { get; init; }

    /// <summary>
    /// Gets or sets the purchase URL.
    /// </summary>
    public string? PurchaseUrl { get; init; }
}

/// <summary>
/// Handler for AddGiftIdeaCommand.
/// </summary>
public class AddGiftIdeaCommandHandler : IRequestHandler<AddGiftIdeaCommand, GiftDto>
{
    private readonly IAnniversaryBirthdayReminderContext _context;
    private readonly ILogger<AddGiftIdeaCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AddGiftIdeaCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public AddGiftIdeaCommandHandler(
        IAnniversaryBirthdayReminderContext context,
        ILogger<AddGiftIdeaCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<GiftDto> Handle(AddGiftIdeaCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Adding gift idea for important date {ImportantDateId}",
            request.ImportantDateId);

        var gift = new Gift
        {
            GiftId = Guid.NewGuid(),
            ImportantDateId = request.ImportantDateId,
            Description = request.Description,
            EstimatedPrice = request.EstimatedPrice,
            PurchaseUrl = request.PurchaseUrl,
            Status = GiftStatus.Idea,
        };

        _context.Gifts.Add(gift);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Added gift idea {GiftId} for important date {ImportantDateId}",
            gift.GiftId,
            request.ImportantDateId);

        return gift.ToDto();
    }
}
