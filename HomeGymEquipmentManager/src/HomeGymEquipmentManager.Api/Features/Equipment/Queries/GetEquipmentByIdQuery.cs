// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeGymEquipmentManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeGymEquipmentManager.Api.Features.Equipment.Queries;

public class GetEquipmentByIdQuery : IRequest<EquipmentDto?>
{
    public Guid EquipmentId { get; set; }
}

public class GetEquipmentByIdQueryHandler : IRequestHandler<GetEquipmentByIdQuery, EquipmentDto?>
{
    private readonly IHomeGymEquipmentManagerContext _context;

    public GetEquipmentByIdQueryHandler(IHomeGymEquipmentManagerContext context)
    {
        _context = context;
    }

    public async Task<EquipmentDto?> Handle(GetEquipmentByIdQuery request, CancellationToken cancellationToken)
    {
        var equipment = await _context.Equipment
            .FirstOrDefaultAsync(e => e.EquipmentId == request.EquipmentId, cancellationToken);

        return equipment == null ? null : EquipmentDto.FromEntity(equipment);
    }
}
