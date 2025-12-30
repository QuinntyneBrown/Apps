// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanBuilder.Core;

namespace WorkoutPlanBuilder.Api.Features.ProgressRecords;

/// <summary>
/// Query to get all progress records.
/// </summary>
public class GetAllProgressRecordsQuery : IRequest<List<ProgressRecordDto>>
{
}

/// <summary>
/// Handler for GetAllProgressRecordsQuery.
/// </summary>
public class GetAllProgressRecordsQueryHandler : IRequestHandler<GetAllProgressRecordsQuery, List<ProgressRecordDto>>
{
    private readonly IWorkoutPlanBuilderContext _context;

    public GetAllProgressRecordsQueryHandler(IWorkoutPlanBuilderContext context)
    {
        _context = context;
    }

    public async Task<List<ProgressRecordDto>> Handle(GetAllProgressRecordsQuery request, CancellationToken cancellationToken)
    {
        return await _context.ProgressRecords
            .OrderByDescending(pr => pr.CompletedAt)
            .Select(pr => pr.ToDto())
            .ToListAsync(cancellationToken);
    }
}
