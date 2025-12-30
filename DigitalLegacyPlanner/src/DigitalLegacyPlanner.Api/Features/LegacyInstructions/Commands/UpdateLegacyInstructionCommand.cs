// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DigitalLegacyPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DigitalLegacyPlanner.Api.Features.LegacyInstructions.Commands;

/// <summary>
/// Command to update an existing legacy instruction.
/// </summary>
public class UpdateLegacyInstructionCommand : IRequest<LegacyInstructionDto?>
{
    public Guid LegacyInstructionId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? Category { get; set; }
    public int Priority { get; set; }
    public string? AssignedTo { get; set; }
    public string? ExecutionTiming { get; set; }
}

/// <summary>
/// Handler for UpdateLegacyInstructionCommand.
/// </summary>
public class UpdateLegacyInstructionCommandHandler : IRequestHandler<UpdateLegacyInstructionCommand, LegacyInstructionDto?>
{
    private readonly IDigitalLegacyPlannerContext _context;

    public UpdateLegacyInstructionCommandHandler(IDigitalLegacyPlannerContext context)
    {
        _context = context;
    }

    public async Task<LegacyInstructionDto?> Handle(UpdateLegacyInstructionCommand request, CancellationToken cancellationToken)
    {
        var instruction = await _context.Instructions
            .FirstOrDefaultAsync(i => i.LegacyInstructionId == request.LegacyInstructionId, cancellationToken);

        if (instruction == null)
        {
            return null;
        }

        instruction.Title = request.Title;
        instruction.UpdateContent(request.Content);
        instruction.Category = request.Category;
        instruction.Priority = request.Priority;
        instruction.AssignedTo = request.AssignedTo;
        instruction.ExecutionTiming = request.ExecutionTiming;

        await _context.SaveChangesAsync(cancellationToken);

        return new LegacyInstructionDto
        {
            LegacyInstructionId = instruction.LegacyInstructionId,
            UserId = instruction.UserId,
            Title = instruction.Title,
            Content = instruction.Content,
            Category = instruction.Category,
            Priority = instruction.Priority,
            AssignedTo = instruction.AssignedTo,
            ExecutionTiming = instruction.ExecutionTiming,
            CreatedAt = instruction.CreatedAt,
            LastUpdatedAt = instruction.LastUpdatedAt
        };
    }
}
