// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using KidsActivitySportsTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KidsActivitySportsTracker.Api.Features.Carpool;

/// <summary>
/// Command to update an existing carpool.
/// </summary>
public record UpdateCarpoolCommand : IRequest<CarpoolDto>
{
    public Guid CarpoolId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? DriverName { get; init; }
    public string? DriverContact { get; init; }
    public DateTime? PickupTime { get; init; }
    public string? PickupLocation { get; init; }
    public DateTime? DropoffTime { get; init; }
    public string? DropoffLocation { get; init; }
    public string? Participants { get; init; }
    public string? Notes { get; init; }
}

/// <summary>
/// Handler for updating a carpool.
/// </summary>
public class UpdateCarpoolCommandHandler : IRequestHandler<UpdateCarpoolCommand, CarpoolDto>
{
    private readonly IKidsActivitySportsTrackerContext _context;

    public UpdateCarpoolCommandHandler(IKidsActivitySportsTrackerContext context)
    {
        _context = context;
    }

    public async Task<CarpoolDto> Handle(UpdateCarpoolCommand request, CancellationToken cancellationToken)
    {
        var carpool = await _context.Carpools
            .FirstOrDefaultAsync(c => c.CarpoolId == request.CarpoolId, cancellationToken);

        if (carpool == null)
        {
            throw new InvalidOperationException($"Carpool with ID {request.CarpoolId} not found.");
        }

        carpool.Name = request.Name;
        carpool.DriverName = request.DriverName;
        carpool.DriverContact = request.DriverContact;
        carpool.PickupTime = request.PickupTime;
        carpool.PickupLocation = request.PickupLocation;
        carpool.DropoffTime = request.DropoffTime;
        carpool.DropoffLocation = request.DropoffLocation;
        carpool.Participants = request.Participants;
        carpool.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        return carpool.ToDto();
    }
}
