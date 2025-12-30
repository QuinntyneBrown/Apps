// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeGymEquipmentManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeGymEquipmentManager.Api.Features.Equipment.Commands;

public class DeleteEquipmentCommand : IRequest<Unit>
{
    public Guid EquipmentId { get; set; }
}

public class DeleteEquipmentCommandHandler : IRequestHandler<DeleteEquipmentCommand, Unit>
{
    private readonly IHomeGymEquipmentManagerContext _context;

    public DeleteEquipmentCommandHandler(IHomeGymEquipmentManagerContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteEquipmentCommand request, CancellationToken cancellationToken)
    {
        var equipment = await _context.Equipment
            .FirstOrDefaultAsync(e => e.EquipmentId == request.EquipmentId, cancellationToken);

        if (equipment == null)
        {
            throw new KeyNotFoundException($"Equipment with ID {request.EquipmentId} not found.");
        }

        _context.Equipment.Remove(equipment);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
