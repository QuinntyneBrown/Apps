// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MedicationReminderSystem.Core;
using Microsoft.EntityFrameworkCore;

namespace MedicationReminderSystem.Api.Features.Refill;

/// <summary>
/// Command to update an existing refill.
/// </summary>
public record UpdateRefillCommand : IRequest<RefillDto>
{
    public Guid RefillId { get; init; }
    public DateTime RefillDate { get; init; }
    public int Quantity { get; init; }
    public string? PharmacyName { get; init; }
    public decimal? Cost { get; init; }
    public DateTime? NextRefillDate { get; init; }
    public int? RefillsRemaining { get; init; }
    public string? Notes { get; init; }
}

/// <summary>
/// Handler for UpdateRefillCommand.
/// </summary>
public class UpdateRefillCommandHandler : IRequestHandler<UpdateRefillCommand, RefillDto>
{
    private readonly IMedicationReminderSystemContext _context;
    private readonly ILogger<UpdateRefillCommandHandler> _logger;

    public UpdateRefillCommandHandler(
        IMedicationReminderSystemContext context,
        ILogger<UpdateRefillCommandHandler> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<RefillDto> Handle(UpdateRefillCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating refill: {RefillId}", request.RefillId);

        var refill = await _context.Refills
            .FirstOrDefaultAsync(r => r.RefillId == request.RefillId, cancellationToken);

        if (refill == null)
        {
            throw new InvalidOperationException($"Refill with ID {request.RefillId} not found.");
        }

        refill.RefillDate = request.RefillDate;
        refill.Quantity = request.Quantity;
        refill.PharmacyName = request.PharmacyName;
        refill.Cost = request.Cost;
        refill.NextRefillDate = request.NextRefillDate;
        refill.RefillsRemaining = request.RefillsRemaining;
        refill.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Refill updated: {RefillId}", refill.RefillId);

        return refill.ToDto();
    }
}
