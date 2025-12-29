// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnniversaryBirthdayReminder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AnniversaryBirthdayReminder.Api;

/// <summary>
/// Query to get an important date by ID.
/// </summary>
public record GetDateByIdQuery : IRequest<ImportantDateDto?>
{
    /// <summary>
    /// Gets or sets the important date ID.
    /// </summary>
    public Guid ImportantDateId { get; init; }
}

/// <summary>
/// Handler for GetDateByIdQuery.
/// </summary>
public class GetDateByIdQueryHandler : IRequestHandler<GetDateByIdQuery, ImportantDateDto?>
{
    private readonly IAnniversaryBirthdayReminderContext _context;
    private readonly ILogger<GetDateByIdQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDateByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetDateByIdQueryHandler(
        IAnniversaryBirthdayReminderContext context,
        ILogger<GetDateByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<ImportantDateDto?> Handle(GetDateByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting important date {ImportantDateId}",
            request.ImportantDateId);

        var importantDate = await _context.ImportantDates
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ImportantDateId == request.ImportantDateId, cancellationToken);

        if (importantDate == null)
        {
            _logger.LogInformation(
                "Important date {ImportantDateId} not found",
                request.ImportantDateId);
            return null;
        }

        return importantDate.ToDto();
    }
}
