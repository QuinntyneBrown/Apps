// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FreelanceProjectManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelanceProjectManager.Api.Features.TimeEntries;

/// <summary>
/// Query to get a time entry by ID.
/// </summary>
public class GetTimeEntryByIdQuery : IRequest<TimeEntryDto?>
{
    /// <summary>
    /// Gets or sets the time entry ID.
    /// </summary>
    public Guid TimeEntryId { get; set; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }
}

/// <summary>
/// Handler for getting a time entry by ID.
/// </summary>
public class GetTimeEntryByIdHandler : IRequestHandler<GetTimeEntryByIdQuery, TimeEntryDto?>
{
    private readonly IFreelanceProjectManagerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetTimeEntryByIdHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetTimeEntryByIdHandler(IFreelanceProjectManagerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<TimeEntryDto?> Handle(GetTimeEntryByIdQuery request, CancellationToken cancellationToken)
    {
        var timeEntry = await _context.TimeEntries
            .Where(te => te.TimeEntryId == request.TimeEntryId && te.UserId == request.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (timeEntry == null)
        {
            return null;
        }

        return new TimeEntryDto
        {
            TimeEntryId = timeEntry.TimeEntryId,
            UserId = timeEntry.UserId,
            ProjectId = timeEntry.ProjectId,
            WorkDate = timeEntry.WorkDate,
            Hours = timeEntry.Hours,
            Description = timeEntry.Description,
            IsBillable = timeEntry.IsBillable,
            IsInvoiced = timeEntry.IsInvoiced,
            InvoiceId = timeEntry.InvoiceId,
            CreatedAt = timeEntry.CreatedAt,
            UpdatedAt = timeEntry.UpdatedAt
        };
    }
}
