// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FreelanceProjectManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelanceProjectManager.Api.Features.Projects;

/// <summary>
/// Command to update a project.
/// </summary>
public class UpdateProjectCommand : IRequest<ProjectDto?>
{
    /// <summary>
    /// Gets or sets the project ID.
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the client ID.
    /// </summary>
    public Guid ClientId { get; set; }

    /// <summary>
    /// Gets or sets the project name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    public ProjectStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the start date.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the due date.
    /// </summary>
    public DateTime? DueDate { get; set; }

    /// <summary>
    /// Gets or sets the hourly rate.
    /// </summary>
    public decimal? HourlyRate { get; set; }

    /// <summary>
    /// Gets or sets the fixed budget.
    /// </summary>
    public decimal? FixedBudget { get; set; }

    /// <summary>
    /// Gets or sets the currency.
    /// </summary>
    public string Currency { get; set; } = "USD";

    /// <summary>
    /// Gets or sets notes.
    /// </summary>
    public string? Notes { get; set; }
}

/// <summary>
/// Handler for updating a project.
/// </summary>
public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, ProjectDto?>
{
    private readonly IFreelanceProjectManagerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateProjectHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public UpdateProjectHandler(IFreelanceProjectManagerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<ProjectDto?> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects
            .Where(p => p.ProjectId == request.ProjectId && p.UserId == request.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (project == null)
        {
            return null;
        }

        project.ClientId = request.ClientId;
        project.Name = request.Name;
        project.Description = request.Description;
        project.UpdateStatus(request.Status);
        project.StartDate = request.StartDate;
        project.DueDate = request.DueDate;
        project.HourlyRate = request.HourlyRate;
        project.FixedBudget = request.FixedBudget;
        project.Currency = request.Currency;
        project.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        return new ProjectDto
        {
            ProjectId = project.ProjectId,
            UserId = project.UserId,
            ClientId = project.ClientId,
            Name = project.Name,
            Description = project.Description,
            Status = project.Status,
            StartDate = project.StartDate,
            DueDate = project.DueDate,
            CompletionDate = project.CompletionDate,
            HourlyRate = project.HourlyRate,
            FixedBudget = project.FixedBudget,
            Currency = project.Currency,
            Notes = project.Notes,
            CreatedAt = project.CreatedAt,
            UpdatedAt = project.UpdatedAt
        };
    }
}
