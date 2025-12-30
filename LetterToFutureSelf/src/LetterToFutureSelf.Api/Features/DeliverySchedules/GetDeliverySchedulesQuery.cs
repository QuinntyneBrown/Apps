// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using LetterToFutureSelf.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LetterToFutureSelf.Api;

/// <summary>
/// Query to get all delivery schedules for a letter.
/// </summary>
public record GetDeliverySchedulesQuery : IRequest<IEnumerable<DeliveryScheduleDto>>
{
    /// <summary>
    /// Gets or sets the letter ID.
    /// </summary>
    public Guid LetterId { get; init; }
}

/// <summary>
/// Handler for GetDeliverySchedulesQuery.
/// </summary>
public class GetDeliverySchedulesQueryHandler : IRequestHandler<GetDeliverySchedulesQuery, IEnumerable<DeliveryScheduleDto>>
{
    private readonly ILetterToFutureSelfContext _context;
    private readonly ILogger<GetDeliverySchedulesQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDeliverySchedulesQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetDeliverySchedulesQueryHandler(
        ILetterToFutureSelfContext context,
        ILogger<GetDeliverySchedulesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<DeliveryScheduleDto>> Handle(GetDeliverySchedulesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting all delivery schedules for letter {LetterId}",
            request.LetterId);

        var schedules = await _context.DeliverySchedules
            .AsNoTracking()
            .Where(x => x.LetterId == request.LetterId)
            .OrderBy(x => x.ScheduledDateTime)
            .ToListAsync(cancellationToken);

        return schedules.Select(x => x.ToDto());
    }
}
