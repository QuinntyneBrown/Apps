// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using LetterToFutureSelf.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LetterToFutureSelf.Api;

/// <summary>
/// Command to create a new delivery schedule.
/// </summary>
public record CreateDeliveryScheduleCommand : IRequest<DeliveryScheduleDto>
{
    /// <summary>
    /// Gets or sets the letter ID.
    /// </summary>
    public Guid LetterId { get; init; }

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
/// Handler for CreateDeliveryScheduleCommand.
/// </summary>
public class CreateDeliveryScheduleCommandHandler : IRequestHandler<CreateDeliveryScheduleCommand, DeliveryScheduleDto>
{
    private readonly ILetterToFutureSelfContext _context;
    private readonly ILogger<CreateDeliveryScheduleCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateDeliveryScheduleCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public CreateDeliveryScheduleCommandHandler(
        ILetterToFutureSelfContext context,
        ILogger<CreateDeliveryScheduleCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<DeliveryScheduleDto> Handle(CreateDeliveryScheduleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating delivery schedule for letter {LetterId}",
            request.LetterId);

        var deliverySchedule = new DeliverySchedule
        {
            DeliveryScheduleId = Guid.NewGuid(),
            LetterId = request.LetterId,
            ScheduledDateTime = request.ScheduledDateTime,
            DeliveryMethod = request.DeliveryMethod,
            RecipientContact = request.RecipientContact,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        _context.DeliverySchedules.Add(deliverySchedule);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created delivery schedule {DeliveryScheduleId} for letter {LetterId}",
            deliverySchedule.DeliveryScheduleId,
            request.LetterId);

        return deliverySchedule.ToDto();
    }
}
