// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CarModificationPartsDatabase.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarModificationPartsDatabase.Api;

/// <summary>
/// Command to delete an installation.
/// </summary>
public record DeleteInstallationCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the installation ID.
    /// </summary>
    public Guid InstallationId { get; init; }
}

/// <summary>
/// Handler for DeleteInstallationCommand.
/// </summary>
public class DeleteInstallationCommandHandler : IRequestHandler<DeleteInstallationCommand, bool>
{
    private readonly ICarModificationPartsDatabaseContext _context;
    private readonly ILogger<DeleteInstallationCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteInstallationCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public DeleteInstallationCommandHandler(
        ICarModificationPartsDatabaseContext context,
        ILogger<DeleteInstallationCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<bool> Handle(DeleteInstallationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting installation {InstallationId}", request.InstallationId);

        var installation = await _context.Installations
            .FirstOrDefaultAsync(i => i.InstallationId == request.InstallationId, cancellationToken);

        if (installation == null)
        {
            _logger.LogWarning("Installation {InstallationId} not found", request.InstallationId);
            return false;
        }

        _context.Installations.Remove(installation);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted installation {InstallationId}", request.InstallationId);

        return true;
    }
}
