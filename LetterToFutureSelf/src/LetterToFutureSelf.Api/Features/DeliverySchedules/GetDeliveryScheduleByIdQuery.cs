// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using LetterToFutureSelf.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LetterToFutureSelf.Api;

/// <summary>
/// Query to get a delivery schedule by ID.
/// </summary>
public record GetDeliveryScheduleByIdQuery : IRequest<DeliveryScheduleDto?>
{
    /// <summary>
    /// Gets or sets the delivery schedule ID.
    /// </summary>
    public Guid DeliveryScheduleId { get; init; }
}

/// <summary>
/// Handler for GetDeliveryScheduleByIdQuery.
/// </summary>
public class GetDeliveryScheduleByIdQueryHandler : IRequestHandler<GetDeliveryScheduleByIdQuery, DeliveryScheduleDto?>
{
    private readonly ILetterToFutureSelfContext _context;
    private readonly ILogger<GetDeliveryScheduleByIdQueryHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDeliveryScheduleByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public GetDeliveryScheduleByIdQueryHandler(
        ILetterToFutureSelfContext context,
        ILogger<GetDeliveryScheduleByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<DeliveryScheduleDto?> Handle(GetDeliveryScheduleByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting delivery schedule {DeliveryScheduleId}",
            request.DeliveryScheduleId);

        var schedule = await _context.DeliverySchedules
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.DeliveryScheduleId == request.DeliveryScheduleId, cancellationToken);

        return schedule?.ToDto();
    }
}
