// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CarModificationPartsDatabase.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarModificationPartsDatabase.Api;

/// <summary>
/// Command to delete a modification.
/// </summary>
public record DeleteModificationCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the modification ID.
    /// </summary>
    public Guid ModificationId { get; init; }
}

/// <summary>
/// Handler for DeleteModificationCommand.
/// </summary>
public class DeleteModificationCommandHandler : IRequestHandler<DeleteModificationCommand, bool>
{
    private readonly ICarModificationPartsDatabaseContext _context;
    private readonly ILogger<DeleteModificationCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteModificationCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public DeleteModificationCommandHandler(
        ICarModificationPartsDatabaseContext context,
        ILogger<DeleteModificationCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<bool> Handle(DeleteModificationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting modification {ModificationId}", request.ModificationId);

        var modification = await _context.Modifications
            .FirstOrDefaultAsync(m => m.ModificationId == request.ModificationId, cancellationToken);

        if (modification == null)
        {
            _logger.LogWarning("Modification {ModificationId} not found", request.ModificationId);
            return false;
        }

        _context.Modifications.Remove(modification);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted modification {ModificationId}", request.ModificationId);

        return true;
    }
}
