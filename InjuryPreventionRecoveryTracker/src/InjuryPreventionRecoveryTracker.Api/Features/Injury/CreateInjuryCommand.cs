// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using InjuryPreventionRecoveryTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InjuryPreventionRecoveryTracker.Api;

/// <summary>
/// Command to create a new injury.
/// </summary>
public record CreateInjuryCommand : IRequest<InjuryDto>
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets or sets the injury type.
    /// </summary>
    public InjuryType InjuryType { get; init; }

    /// <summary>
    /// Gets or sets the injury severity.
    /// </summary>
    public InjurySeverity Severity { get; init; }

    /// <summary>
    /// Gets or sets the body part affected.
    /// </summary>
    public string BodyPart { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the date when the injury occurred.
    /// </summary>
    public DateTime InjuryDate { get; init; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Gets or sets the diagnosis.
    /// </summary>
    public string? Diagnosis { get; init; }

    /// <summary>
    /// Gets or sets the expected recovery days.
    /// </summary>
    public int? ExpectedRecoveryDays { get; init; }

    /// <summary>
    /// Gets or sets the notes.
    /// </summary>
    public string? Notes { get; init; }
}

/// <summary>
/// Handler for CreateInjuryCommand.
/// </summary>
public class CreateInjuryCommandHandler : IRequestHandler<CreateInjuryCommand, InjuryDto>
{
    private readonly IInjuryPreventionRecoveryTrackerContext _context;
    private readonly ILogger<CreateInjuryCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateInjuryCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public CreateInjuryCommandHandler(
        IInjuryPreventionRecoveryTrackerContext context,
        ILogger<CreateInjuryCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<InjuryDto> Handle(CreateInjuryCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating injury for user {UserId}, body part {BodyPart}",
            request.UserId,
            request.BodyPart);

        var injury = new Injury
        {
            InjuryId = Guid.NewGuid(),
            UserId = request.UserId,
            InjuryType = request.InjuryType,
            Severity = request.Severity,
            BodyPart = request.BodyPart,
            InjuryDate = request.InjuryDate,
            Description = request.Description,
            Diagnosis = request.Diagnosis,
            ExpectedRecoveryDays = request.ExpectedRecoveryDays,
            Status = "Active",
            ProgressPercentage = 0,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Injuries.Add(injury);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created injury {InjuryId} for user {UserId}",
            injury.InjuryId,
            request.UserId);

        return injury.ToDto();
    }
}
