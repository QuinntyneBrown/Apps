// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DigitalLegacyPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DigitalLegacyPlanner.Api.Features.LegacyInstructions.Commands;

/// <summary>
/// Command to delete a legacy instruction.
/// </summary>
public class DeleteLegacyInstructionCommand : IRequest<bool>
{
    public Guid LegacyInstructionId { get; set; }
}

/// <summary>
/// Handler for DeleteLegacyInstructionCommand.
/// </summary>
public class DeleteLegacyInstructionCommandHandler : IRequestHandler<DeleteLegacyInstructionCommand, bool>
{
    private readonly IDigitalLegacyPlannerContext _context;

    public DeleteLegacyInstructionCommandHandler(IDigitalLegacyPlannerContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteLegacyInstructionCommand request, CancellationToken cancellationToken)
    {
        var instruction = await _context.Instructions
            .FirstOrDefaultAsync(i => i.LegacyInstructionId == request.LegacyInstructionId, cancellationToken);

        if (instruction == null)
        {
            return false;
        }

        _context.Instructions.Remove(instruction);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
