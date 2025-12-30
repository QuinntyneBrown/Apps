// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using LetterToFutureSelf.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LetterToFutureSelf.Api;

/// <summary>
/// Command to delete a delivery schedule.
/// </summary>
public record DeleteDeliveryScheduleCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the delivery schedule ID.
    /// </summary>
    public Guid DeliveryScheduleId { get; init; }
}

/// <summary>
/// Handler for DeleteDeliveryScheduleCommand.
/// </summary>
public class DeleteDeliveryScheduleCommandHandler : IRequestHandler<DeleteDeliveryScheduleCommand, bool>
{
    private readonly ILetterToFutureSelfContext _context;
    private readonly ILogger<DeleteDeliveryScheduleCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteDeliveryScheduleCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public DeleteDeliveryScheduleCommandHandler(
        ILetterToFutureSelfContext context,
        ILogger<DeleteDeliveryScheduleCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<bool> Handle(DeleteDeliveryScheduleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting delivery schedule {DeliveryScheduleId}", request.DeliveryScheduleId);

        var deliverySchedule = await _context.DeliverySchedules
            .FirstOrDefaultAsync(x => x.DeliveryScheduleId == request.DeliveryScheduleId, cancellationToken);

        if (deliverySchedule == null)
        {
            _logger.LogWarning("Delivery schedule {DeliveryScheduleId} not found", request.DeliveryScheduleId);
            return false;
        }

        _context.DeliverySchedules.Remove(deliverySchedule);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted delivery schedule {DeliveryScheduleId}", request.DeliveryScheduleId);

        return true;
    }
}
