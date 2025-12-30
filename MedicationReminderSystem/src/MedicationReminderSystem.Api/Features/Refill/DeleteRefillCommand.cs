// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using MedicationReminderSystem.Core;
using Microsoft.EntityFrameworkCore;

namespace MedicationReminderSystem.Api.Features.Refill;

/// <summary>
/// Command to delete a refill.
/// </summary>
public record DeleteRefillCommand(Guid RefillId) : IRequest<Unit>;

/// <summary>
/// Handler for DeleteRefillCommand.
/// </summary>
public class DeleteRefillCommandHandler : IRequestHandler<DeleteRefillCommand, Unit>
{
    private readonly IMedicationReminderSystemContext _context;
    private readonly ILogger<DeleteRefillCommandHandler> _logger;

    public DeleteRefillCommandHandler(
        IMedicationReminderSystemContext context,
        ILogger<DeleteRefillCommandHandler> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<Unit> Handle(DeleteRefillCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting refill: {RefillId}", request.RefillId);

        var refill = await _context.Refills
            .FirstOrDefaultAsync(r => r.RefillId == request.RefillId, cancellationToken);

        if (refill == null)
        {
            throw new InvalidOperationException($"Refill with ID {request.RefillId} not found.");
        }

        _context.Refills.Remove(refill);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Refill deleted: {RefillId}", request.RefillId);

        return Unit.Value;
    }
}
