// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CarModificationPartsDatabase.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarModificationPartsDatabase.Api;

/// <summary>
/// Command to delete a part.
/// </summary>
public record DeletePartCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the part ID.
    /// </summary>
    public Guid PartId { get; init; }
}

/// <summary>
/// Handler for DeletePartCommand.
/// </summary>
public class DeletePartCommandHandler : IRequestHandler<DeletePartCommand, bool>
{
    private readonly ICarModificationPartsDatabaseContext _context;
    private readonly ILogger<DeletePartCommandHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeletePartCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    public DeletePartCommandHandler(
        ICarModificationPartsDatabaseContext context,
        ILogger<DeletePartCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<bool> Handle(DeletePartCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting part {PartId}", request.PartId);

        var part = await _context.Parts
            .FirstOrDefaultAsync(p => p.PartId == request.PartId, cancellationToken);

        if (part == null)
        {
            _logger.LogWarning("Part {PartId} not found", request.PartId);
            return false;
        }

        _context.Parts.Remove(part);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted part {PartId}", request.PartId);

        return true;
    }
}
