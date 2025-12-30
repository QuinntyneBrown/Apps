// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InjuryPreventionRecoveryTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InjuryPreventionRecoveryTracker.Api;

/// <summary>
/// Command to create a new milestone.
/// </summary>
public record CreateMilestoneCommand : IRequest<MilestoneDto>
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets or sets the injury ID.
    /// </summary>
    public Guid InjuryId { get; init; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Gets or sets the target date.
    /// </summary>
    public DateTime? TargetDate { get; init; }

    /// <summary>
    /// Gets or sets the notes.
    /// </summary>
    public string? Notes { get; init; }
}

/// <summary>
/// Handler for CreateMilestoneCommand.
/// </summary>
public class CreateMilestoneCommandHandler : IRequestHandler<CreateMilestoneCommand, MilestoneDto>
{
    private readonly IInjuryPreventionRecoveryTrackerContext _context;
    private readonly ILogger<CreateMilestoneCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateMilestoneCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public CreateMilestoneCommandHandler(
        IInjuryPreventionRecoveryTrackerContext context,
        ILogger<CreateMilestoneCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<MilestoneDto> Handle(CreateMilestoneCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating milestone for injury {InjuryId}, name {Name}",
            request.InjuryId,
            request.Name);

        var milestone = new Milestone
        {
            MilestoneId = Guid.NewGuid(),
            UserId = request.UserId,
            InjuryId = request.InjuryId,
            Name = request.Name,
            Description = request.Description,
            TargetDate = request.TargetDate,
            IsAchieved = false,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Milestones.Add(milestone);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created milestone {MilestoneId} for injury {InjuryId}",
            milestone.MilestoneId,
            request.InjuryId);

        return milestone.ToDto();
    }
}
