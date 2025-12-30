// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MedicationReminderSystem.Core;
using Microsoft.EntityFrameworkCore;

namespace MedicationReminderSystem.Api.Features.Refill;

/// <summary>
/// Command to create a new refill.
/// </summary>
public record CreateRefillCommand : IRequest<RefillDto>
{
    public Guid UserId { get; init; }
    public Guid MedicationId { get; init; }
    public DateTime RefillDate { get; init; }
    public int Quantity { get; init; }
    public string? PharmacyName { get; init; }
    public decimal? Cost { get; init; }
    public DateTime? NextRefillDate { get; init; }
    public int? RefillsRemaining { get; init; }
    public string? Notes { get; init; }
}

/// <summary>
/// Handler for CreateRefillCommand.
/// </summary>
public class CreateRefillCommandHandler : IRequestHandler<CreateRefillCommand, RefillDto>
{
    private readonly IMedicationReminderSystemContext _context;
    private readonly ILogger<CreateRefillCommandHandler> _logger;

    public CreateRefillCommandHandler(
        IMedicationReminderSystemContext context,
        ILogger<CreateRefillCommandHandler> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<RefillDto> Handle(CreateRefillCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating refill for medication: {MedicationId}", request.MedicationId);

        var refill = new Core.Refill
        {
            RefillId = Guid.NewGuid(),
            UserId = request.UserId,
            MedicationId = request.MedicationId,
            RefillDate = request.RefillDate,
            Quantity = request.Quantity,
            PharmacyName = request.PharmacyName,
            Cost = request.Cost,
            NextRefillDate = request.NextRefillDate,
            RefillsRemaining = request.RefillsRemaining,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };

        _context.Refills.Add(refill);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Refill created: {RefillId}", refill.RefillId);

        return refill.ToDto();
    }
}
