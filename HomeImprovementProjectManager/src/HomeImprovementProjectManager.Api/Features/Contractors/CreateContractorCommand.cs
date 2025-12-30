// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeImprovementProjectManager.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HomeImprovementProjectManager.Api;

/// <summary>
/// Command to create a new contractor.
/// </summary>
public record CreateContractorCommand : IRequest<ContractorDto>
{
    /// <summary>
    /// Gets or sets the project ID.
    /// </summary>
    public Guid? ProjectId { get; init; }

    /// <summary>
    /// Gets or sets the contractor name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the trade.
    /// </summary>
    public string? Trade { get; init; }

    /// <summary>
    /// Gets or sets the phone number.
    /// </summary>
    public string? PhoneNumber { get; init; }

    /// <summary>
    /// Gets or sets the email.
    /// </summary>
    public string? Email { get; init; }

    /// <summary>
    /// Gets or sets the rating.
    /// </summary>
    public int? Rating { get; init; }
}

/// <summary>
/// Handler for CreateContractorCommand.
/// </summary>
public class CreateContractorCommandHandler : IRequestHandler<CreateContractorCommand, ContractorDto>
{
    private readonly IHomeImprovementProjectManagerContext _context;
    private readonly ILogger<CreateContractorCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateContractorCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public CreateContractorCommandHandler(
        IHomeImprovementProjectManagerContext context,
        ILogger<CreateContractorCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<ContractorDto> Handle(CreateContractorCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating contractor {Name}, trade {Trade}",
            request.Name,
            request.Trade);

        var contractor = new Contractor
        {
            ContractorId = Guid.NewGuid(),
            ProjectId = request.ProjectId,
            Name = request.Name,
            Trade = request.Trade,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            Rating = request.Rating,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Contractors.Add(contractor);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created contractor {ContractorId}",
            contractor.ContractorId);

        return contractor.ToDto();
    }
}
