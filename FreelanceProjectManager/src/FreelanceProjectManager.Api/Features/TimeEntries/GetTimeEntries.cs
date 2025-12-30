// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FreelanceProjectManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelanceProjectManager.Api.Features.TimeEntries;

/// <summary>
/// Query to get all time entries for a user.
/// </summary>
public class GetTimeEntriesQuery : IRequest<List<TimeEntryDto>>
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the optional project ID filter.
    /// </summary>
    public Guid? ProjectId { get; set; }
}

/// <summary>
/// Handler for getting time entries.
/// </summary>
public class GetTimeEntriesHandler : IRequestHandler<GetTimeEntriesQuery, List<TimeEntryDto>>
{
    private readonly IFreelanceProjectManagerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetTimeEntriesHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetTimeEntriesHandler(IFreelanceProjectManagerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<List<TimeEntryDto>> Handle(GetTimeEntriesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.TimeEntries
            .Where(te => te.UserId == request.UserId);

        if (request.ProjectId.HasValue)
        {
            query = query.Where(te => te.ProjectId == request.ProjectId.Value);
        }

        return await query
            .Select(te => new TimeEntryDto
            {
                TimeEntryId = te.TimeEntryId,
                UserId = te.UserId,
                ProjectId = te.ProjectId,
                WorkDate = te.WorkDate,
                Hours = te.Hours,
                Description = te.Description,
                IsBillable = te.IsBillable,
                IsInvoiced = te.IsInvoiced,
                InvoiceId = te.InvoiceId,
                CreatedAt = te.CreatedAt,
                UpdatedAt = te.UpdatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
