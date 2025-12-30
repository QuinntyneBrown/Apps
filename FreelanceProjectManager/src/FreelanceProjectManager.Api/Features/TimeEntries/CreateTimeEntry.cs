// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FreelanceProjectManager.Core;
using MediatR;

namespace FreelanceProjectManager.Api.Features.TimeEntries;

/// <summary>
/// Command to create a new time entry.
/// </summary>
public class CreateTimeEntryCommand : IRequest<TimeEntryDto>
{
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
    public bool IsBillable { get; set; } = true;
}

/// <summary>
/// Handler for creating a time entry.
/// </summary>
public class CreateTimeEntryHandler : IRequestHandler<CreateTimeEntryCommand, TimeEntryDto>
{
    private readonly IFreelanceProjectManagerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateTimeEntryHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CreateTimeEntryHandler(IFreelanceProjectManagerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<TimeEntryDto> Handle(CreateTimeEntryCommand request, CancellationToken cancellationToken)
    {
        var timeEntry = new TimeEntry
        {
            TimeEntryId = Guid.NewGuid(),
            UserId = request.UserId,
            ProjectId = request.ProjectId,
            WorkDate = request.WorkDate,
            Hours = request.Hours,
            Description = request.Description,
            IsBillable = request.IsBillable,
            IsInvoiced = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.TimeEntries.Add(timeEntry);
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
