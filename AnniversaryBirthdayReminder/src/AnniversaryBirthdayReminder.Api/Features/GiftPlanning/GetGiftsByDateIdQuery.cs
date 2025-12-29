// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnniversaryBirthdayReminder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AnniversaryBirthdayReminder.Api;

/// <summary>
/// Query to get all gifts for an important date.
/// </summary>
public record GetGiftsByDateIdQuery : IRequest<IEnumerable<GiftDto>>
{
    /// <summary>
    /// Gets or sets the important date ID.
    /// </summary>
    public Guid ImportantDateId { get; init; }
}

/// <summary>
/// Handler for GetGiftsByDateIdQuery.
/// </summary>
public class GetGiftsByDateIdQueryHandler : IRequestHandler<GetGiftsByDateIdQuery, IEnumerable<GiftDto>>
{
    private readonly IAnniversaryBirthdayReminderContext _context;
    private readonly ILogger<GetGiftsByDateIdQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetGiftsByDateIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetGiftsByDateIdQueryHandler(
        IAnniversaryBirthdayReminderContext context,
        ILogger<GetGiftsByDateIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<GiftDto>> Handle(GetGiftsByDateIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting gifts for important date {ImportantDateId}",
            request.ImportantDateId);

        var gifts = await _context.Gifts
            .AsNoTracking()
            .Where(x => x.ImportantDateId == request.ImportantDateId)
            .OrderByDescending(x => x.Status)
            .ToListAsync(cancellationToken);

        return gifts.Select(x => x.ToDto());
    }
}
