// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeGymEquipmentManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeGymEquipmentManager.Api.Features.Equipment.Commands;

public class UpdateEquipmentCommand : IRequest<EquipmentDto>
{
    public Guid EquipmentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public EquipmentType EquipmentType { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public decimal? PurchasePrice { get; set; }
    public string? Location { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; }
}

public class UpdateEquipmentCommandHandler : IRequestHandler<UpdateEquipmentCommand, EquipmentDto>
{
    private readonly IHomeGymEquipmentManagerContext _context;

    public UpdateEquipmentCommandHandler(IHomeGymEquipmentManagerContext context)
    {
        _context = context;
    }

    public async Task<EquipmentDto> Handle(UpdateEquipmentCommand request, CancellationToken cancellationToken)
    {
        var equipment = await _context.Equipment
            .FirstOrDefaultAsync(e => e.EquipmentId == request.EquipmentId, cancellationToken);

        if (equipment == null)
        {
            throw new KeyNotFoundException($"Equipment with ID {request.EquipmentId} not found.");
        }

        equipment.Name = request.Name;
        equipment.EquipmentType = request.EquipmentType;
        equipment.Brand = request.Brand;
        equipment.Model = request.Model;
        equipment.PurchaseDate = request.PurchaseDate;
        equipment.PurchasePrice = request.PurchasePrice;
        equipment.Location = request.Location;
        equipment.Notes = request.Notes;
        equipment.IsActive = request.IsActive;

        await _context.SaveChangesAsync(cancellationToken);

        return EquipmentDto.FromEntity(equipment);
    }
}
