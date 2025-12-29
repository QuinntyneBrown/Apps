// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnniversaryBirthdayReminder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AnniversaryBirthdayReminder.Api;

/// <summary>
/// Query to get all celebrations for an important date.
/// </summary>
public record GetCelebrationsByDateIdQuery : IRequest<IEnumerable<CelebrationDto>>
{
    /// <summary>
    /// Gets or sets the important date ID.
    /// </summary>
    public Guid ImportantDateId { get; init; }
}

/// <summary>
/// Handler for GetCelebrationsByDateIdQuery.
/// </summary>
public class GetCelebrationsByDateIdQueryHandler : IRequestHandler<GetCelebrationsByDateIdQuery, IEnumerable<CelebrationDto>>
{
    private readonly IAnniversaryBirthdayReminderContext _context;
    private readonly ILogger<GetCelebrationsByDateIdQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetCelebrationsByDateIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetCelebrationsByDateIdQueryHandler(
        IAnniversaryBirthdayReminderContext context,
        ILogger<GetCelebrationsByDateIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<CelebrationDto>> Handle(GetCelebrationsByDateIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting celebrations for important date {ImportantDateId}",
            request.ImportantDateId);

        var celebrations = await _context.Celebrations
            .AsNoTracking()
            .Where(x => x.ImportantDateId == request.ImportantDateId)
            .OrderByDescending(x => x.CelebrationDate)
            .ToListAsync(cancellationToken);

        return celebrations.Select(x => x.ToDto());
    }
}
