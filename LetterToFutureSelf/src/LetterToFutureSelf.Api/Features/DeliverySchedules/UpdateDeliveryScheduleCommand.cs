// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using LetterToFutureSelf.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LetterToFutureSelf.Api;

/// <summary>
/// Command to update an existing delivery schedule.
/// </summary>
public record UpdateDeliveryScheduleCommand : IRequest<DeliveryScheduleDto?>
{
    /// <summary>
    /// Gets or sets the delivery schedule ID.
    /// </summary>
    public Guid DeliveryScheduleId { get; init; }

    /// <summary>
    /// Gets or sets the scheduled date and time.
    /// </summary>
    public DateTime ScheduledDateTime { get; init; }

    /// <summary>
    /// Gets or sets the delivery method.
    /// </summary>
    public string DeliveryMethod { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the recipient contact.
    /// </summary>
    public string? RecipientContact { get; init; }
}

/// <summary>
/// Handler for UpdateDeliveryScheduleCommand.
/// </summary>
public class UpdateDeliveryScheduleCommandHandler : IRequestHandler<UpdateDeliveryScheduleCommand, DeliveryScheduleDto?>
{
    private readonly ILetterToFutureSelfContext _context;
    private readonly ILogger<UpdateDeliveryScheduleCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateDeliveryScheduleCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public UpdateDeliveryScheduleCommandHandler(
        ILetterToFutureSelfContext context,
        ILogger<UpdateDeliveryScheduleCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<DeliveryScheduleDto?> Handle(UpdateDeliveryScheduleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Updating delivery schedule {DeliveryScheduleId}",
            request.DeliveryScheduleId);

        var deliverySchedule = await _context.DeliverySchedules
            .FirstOrDefaultAsync(x => x.DeliveryScheduleId == request.DeliveryScheduleId, cancellationToken);

        if (deliverySchedule == null)
        {
            _logger.LogWarning("Delivery schedule {DeliveryScheduleId} not found", request.DeliveryScheduleId);
            return null;
        }

        deliverySchedule.ScheduledDateTime = request.ScheduledDateTime;
        deliverySchedule.DeliveryMethod = request.DeliveryMethod;
        deliverySchedule.RecipientContact = request.RecipientContact;
        deliverySchedule.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated delivery schedule {DeliveryScheduleId}", request.DeliveryScheduleId);

        return deliverySchedule.ToDto();
    }
}
