// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FreelanceProjectManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelanceProjectManager.Api.Features.TimeEntries;

/// <summary>
/// Command to update a time entry.
/// </summary>
public class UpdateTimeEntryCommand : IRequest<TimeEntryDto?>
{
    /// <summary>
    /// Gets or sets the time entry ID.
    /// </summary>
    public Guid TimeEntryId { get; set; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the project ID.
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    /// Gets or sets the work date.
    /// </summary>
    public DateTime WorkDate { get; set; }

    /// <summary>
    /// Gets or sets the hours.
    /// </summary>
    public decimal Hours { get; set; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether this is billable.
    /// </summary>
    public bool IsBillable { get; set; }
}

/// <summary>
/// Handler for updating a time entry.
/// </summary>
public class UpdateTimeEntryHandler : IRequestHandler<UpdateTimeEntryCommand, TimeEntryDto?>
{
    private readonly IFreelanceProjectManagerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateTimeEntryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public UpdateTimeEntryHandler(IFreelanceProjectManagerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<TimeEntryDto?> Handle(UpdateTimeEntryCommand request, CancellationToken cancellationToken)
    {
        var timeEntry = await _context.TimeEntries
            .Where(te => te.TimeEntryId == request.TimeEntryId && te.UserId == request.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (timeEntry == null)
        {
            return null;
        }

        timeEntry.ProjectId = request.ProjectId;
        timeEntry.WorkDate = request.WorkDate;
        timeEntry.Hours = request.Hours;
        timeEntry.Description = request.Description;
        timeEntry.IsBillable = request.IsBillable;
        timeEntry.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

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
