// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnniversaryBirthdayReminder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AnniversaryBirthdayReminder.Api;

/// <summary>
/// Command to mark a gift as purchased.
/// </summary>
public record MarkGiftPurchasedCommand : IRequest<GiftDto?>
{
    /// <summary>
    /// Gets or sets the gift ID.
    /// </summary>
    public Guid GiftId { get; init; }

    /// <summary>
    /// Gets or sets the actual price.
    /// </summary>
    public decimal ActualPrice { get; init; }
}

/// <summary>
/// Handler for MarkGiftPurchasedCommand.
/// </summary>
public class MarkGiftPurchasedCommandHandler : IRequestHandler<MarkGiftPurchasedCommand, GiftDto?>
{
    private readonly IAnniversaryBirthdayReminderContext _context;
    private readonly ILogger<MarkGiftPurchasedCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="MarkGiftPurchasedCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public MarkGiftPurchasedCommandHandler(
        IAnniversaryBirthdayReminderContext context,
        ILogger<MarkGiftPurchasedCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<GiftDto?> Handle(MarkGiftPurchasedCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Marking gift {GiftId} as purchased",
            request.GiftId);

        var gift = await _context.Gifts
            .FirstOrDefaultAsync(x => x.GiftId == request.GiftId, cancellationToken);

        if (gift == null)
        {
            _logger.LogWarning(
                "Gift {GiftId} not found",
                request.GiftId);
            return null;
        }

        gift.MarkAsPurchased(request.ActualPrice);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Marked gift {GiftId} as purchased for {ActualPrice}",
            request.GiftId,
            request.ActualPrice);

        return gift.ToDto();
    }
}
