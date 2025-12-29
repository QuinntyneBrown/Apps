// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnniversaryBirthdayReminder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AnniversaryBirthdayReminder.Api;

/// <summary>
/// Command to mark a gift as delivered.
/// </summary>
public record MarkGiftDeliveredCommand : IRequest<GiftDto?>
{
    /// <summary>
    /// Gets or sets the gift ID.
    /// </summary>
    public Guid GiftId { get; init; }
}

/// <summary>
/// Handler for MarkGiftDeliveredCommand.
/// </summary>
public class MarkGiftDeliveredCommandHandler : IRequestHandler<MarkGiftDeliveredCommand, GiftDto?>
{
    private readonly IAnniversaryBirthdayReminderContext _context;
    private readonly ILogger<MarkGiftDeliveredCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="MarkGiftDeliveredCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public MarkGiftDeliveredCommandHandler(
        IAnniversaryBirthdayReminderContext context,
        ILogger<MarkGiftDeliveredCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<GiftDto?> Handle(MarkGiftDeliveredCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Marking gift {GiftId} as delivered",
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

        gift.MarkAsDelivered();
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Marked gift {GiftId} as delivered",
            request.GiftId);

        return gift.ToDto();
    }
}
