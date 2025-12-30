// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanBuilder.Core;

namespace WorkoutPlanBuilder.Api.Features.ProgressRecords;

/// <summary>
/// Query to get a progress record by ID.
/// </summary>
public class GetProgressRecordByIdQuery : IRequest<ProgressRecordDto?>
{
    public Guid ProgressRecordId { get; set; }
}

/// <summary>
/// Handler for GetProgressRecordByIdQuery.
/// </summary>
public class GetProgressRecordByIdQueryHandler : IRequestHandler<GetProgressRecordByIdQuery, ProgressRecordDto?>
{
    private readonly IWorkoutPlanBuilderContext _context;

    public GetProgressRecordByIdQueryHandler(IWorkoutPlanBuilderContext context)
    {
        _context = context;
    }

    public async Task<ProgressRecordDto?> Handle(GetProgressRecordByIdQuery request, CancellationToken cancellationToken)
    {
        var progressRecord = await _context.ProgressRecords
            .FirstOrDefaultAsync(pr => pr.ProgressRecordId == request.ProgressRecordId, cancellationToken);

        return progressRecord?.ToDto();
    }
}
