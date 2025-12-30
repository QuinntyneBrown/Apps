// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DigitalLegacyPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DigitalLegacyPlanner.Api.Features.LegacyInstructions.Queries;

/// <summary>
/// Query to get all legacy instructions for a user.
/// </summary>
public class GetLegacyInstructionsQuery : IRequest<List<LegacyInstructionDto>>
{
    public Guid? UserId { get; set; }
    public string? Category { get; set; }
}

/// <summary>
/// Handler for GetLegacyInstructionsQuery.
/// </summary>
public class GetLegacyInstructionsQueryHandler : IRequestHandler<GetLegacyInstructionsQuery, List<LegacyInstructionDto>>
{
    private readonly IDigitalLegacyPlannerContext _context;

    public GetLegacyInstructionsQueryHandler(IDigitalLegacyPlannerContext context)
    {
        _context = context;
    }

    public async Task<List<LegacyInstructionDto>> Handle(GetLegacyInstructionsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Instructions.AsQueryable();

        if (request.UserId.HasValue)
        {
            query = query.Where(i => i.UserId == request.UserId.Value);
        }

        if (!string.IsNullOrEmpty(request.Category))
        {
            query = query.Where(i => i.Category == request.Category);
        }

        var instructions = await query.OrderBy(i => i.Priority).ToListAsync(cancellationToken);

        return instructions.Select(instruction => new LegacyInstructionDto
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
        }).ToList();
    }
}
