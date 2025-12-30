// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FreelanceProjectManager.Core;
using MediatR;

namespace FreelanceProjectManager.Api.Features.Projects;

/// <summary>
/// Command to create a new project.
/// </summary>
public class CreateProjectCommand : IRequest<ProjectDto>
{
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
/// Handler for creating a project.
/// </summary>
public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, ProjectDto>
{
    private readonly IFreelanceProjectManagerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateProjectHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CreateProjectHandler(IFreelanceProjectManagerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<ProjectDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = request.UserId,
            ClientId = request.ClientId,
            Name = request.Name,
            Description = request.Description,
            Status = ProjectStatus.Planning,
            StartDate = request.StartDate,
            DueDate = request.DueDate,
            HourlyRate = request.HourlyRate,
            FixedBudget = request.FixedBudget,
            Currency = request.Currency,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };

        _context.Projects.Add(project);
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
