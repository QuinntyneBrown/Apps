// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DigitalLegacyPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DigitalLegacyPlanner.Api.Features.LegacyInstructions.Queries;

/// <summary>
/// Query to get a legacy instruction by ID.
/// </summary>
public class GetLegacyInstructionByIdQuery : IRequest<LegacyInstructionDto?>
{
    public Guid LegacyInstructionId { get; set; }
}

/// <summary>
/// Handler for GetLegacyInstructionByIdQuery.
/// </summary>
public class GetLegacyInstructionByIdQueryHandler : IRequestHandler<GetLegacyInstructionByIdQuery, LegacyInstructionDto?>
{
    private readonly IDigitalLegacyPlannerContext _context;

    public GetLegacyInstructionByIdQueryHandler(IDigitalLegacyPlannerContext context)
    {
        _context = context;
    }

    public async Task<LegacyInstructionDto?> Handle(GetLegacyInstructionByIdQuery request, CancellationToken cancellationToken)
    {
        var instruction = await _context.Instructions
            .FirstOrDefaultAsync(i => i.LegacyInstructionId == request.LegacyInstructionId, cancellationToken);

        if (instruction == null)
        {
            return null;
        }

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
