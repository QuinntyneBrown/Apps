// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeGymEquipmentManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeGymEquipmentManager.Api.Features.Equipment.Commands;

public class CreateEquipmentCommand : IRequest<EquipmentDto>
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public EquipmentType EquipmentType { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public decimal? PurchasePrice { get; set; }
    public string? Location { get; set; }
    public string? Notes { get; set; }
}

public class CreateEquipmentCommandHandler : IRequestHandler<CreateEquipmentCommand, EquipmentDto>
{
    private readonly IHomeGymEquipmentManagerContext _context;

    public CreateEquipmentCommandHandler(IHomeGymEquipmentManagerContext context)
    {
        _context = context;
    }

    public async Task<EquipmentDto> Handle(CreateEquipmentCommand request, CancellationToken cancellationToken)
    {
        var equipment = new Core.Equipment
        {
            EquipmentId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            EquipmentType = request.EquipmentType,
            Brand = request.Brand,
            Model = request.Model,
            PurchaseDate = request.PurchaseDate,
            PurchasePrice = request.PurchasePrice,
            Location = request.Location,
            Notes = request.Notes,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.Equipment.Add(equipment);
        await _context.SaveChangesAsync(cancellationToken);

        return EquipmentDto.FromEntity(equipment);
    }
}
