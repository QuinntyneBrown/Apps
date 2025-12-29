// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnniversaryBirthdayReminder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AnniversaryBirthdayReminder.Api;

/// <summary>
/// Query to get upcoming important dates for a user.
/// </summary>
public record GetUpcomingDatesQuery : IRequest<IEnumerable<ImportantDateDto>>
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets or sets the number of days to look ahead.
    /// </summary>
    public int DaysAhead { get; init; } = 30;
}

/// <summary>
/// Handler for GetUpcomingDatesQuery.
/// </summary>
public class GetUpcomingDatesQueryHandler : IRequestHandler<GetUpcomingDatesQuery, IEnumerable<ImportantDateDto>>
{
    private readonly IAnniversaryBirthdayReminderContext _context;
    private readonly ILogger<GetUpcomingDatesQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUpcomingDatesQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetUpcomingDatesQueryHandler(
        IAnniversaryBirthdayReminderContext context,
        ILogger<GetUpcomingDatesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ImportantDateDto>> Handle(GetUpcomingDatesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting upcoming dates for user {UserId} within {DaysAhead} days",
            request.UserId,
            request.DaysAhead);

        var dates = await _context.ImportantDates
            .AsNoTracking()
            .Where(x => x.UserId == request.UserId && x.IsActive)
            .ToListAsync(cancellationToken);

        var today = DateTime.UtcNow.Date;
        var endDate = today.AddDays(request.DaysAhead);

        var upcomingDates = dates
            .Select(x => x.ToDto())
            .Where(x => x.NextOccurrence >= today && x.NextOccurrence <= endDate)
            .OrderBy(x => x.NextOccurrence)
            .ToList();

        return upcomingDates;
    }
}
