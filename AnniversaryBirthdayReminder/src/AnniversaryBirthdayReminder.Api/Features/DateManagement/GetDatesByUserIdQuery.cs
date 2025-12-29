// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnniversaryBirthdayReminder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AnniversaryBirthdayReminder.Api;

/// <summary>
/// Query to get all important dates for a user.
/// </summary>
public record GetDatesByUserIdQuery : IRequest<IEnumerable<ImportantDateDto>>
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; init; }
}

/// <summary>
/// Handler for GetDatesByUserIdQuery.
/// </summary>
public class GetDatesByUserIdQueryHandler : IRequestHandler<GetDatesByUserIdQuery, IEnumerable<ImportantDateDto>>
{
    private readonly IAnniversaryBirthdayReminderContext _context;
    private readonly ILogger<GetDatesByUserIdQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDatesByUserIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetDatesByUserIdQueryHandler(
        IAnniversaryBirthdayReminderContext context,
        ILogger<GetDatesByUserIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ImportantDateDto>> Handle(GetDatesByUserIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting all important dates for user {UserId}",
            request.UserId);

        var dates = await _context.ImportantDates
            .AsNoTracking()
            .Where(x => x.UserId == request.UserId)
            .OrderBy(x => x.DateValue)
            .ToListAsync(cancellationToken);

        return dates.Select(x => x.ToDto());
    }
}
