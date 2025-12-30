// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DigitalLegacyPlanner.Core;
using MediatR;

namespace DigitalLegacyPlanner.Api.Features.LegacyInstructions.Commands;

/// <summary>
/// Command to create a new legacy instruction.
/// </summary>
public class CreateLegacyInstructionCommand : IRequest<LegacyInstructionDto>
{
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? Category { get; set; }
    public int Priority { get; set; }
    public string? AssignedTo { get; set; }
    public string? ExecutionTiming { get; set; }
}

/// <summary>
/// Handler for CreateLegacyInstructionCommand.
/// </summary>
public class CreateLegacyInstructionCommandHandler : IRequestHandler<CreateLegacyInstructionCommand, LegacyInstructionDto>
{
    private readonly IDigitalLegacyPlannerContext _context;

    public CreateLegacyInstructionCommandHandler(IDigitalLegacyPlannerContext context)
    {
        _context = context;
    }

    public async Task<LegacyInstructionDto> Handle(CreateLegacyInstructionCommand request, CancellationToken cancellationToken)
    {
        var instruction = new LegacyInstruction
        {
            LegacyInstructionId = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            Content = request.Content,
            Category = request.Category,
            Priority = request.Priority,
            AssignedTo = request.AssignedTo,
            ExecutionTiming = request.ExecutionTiming,
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow
        };

        _context.Instructions.Add(instruction);
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
