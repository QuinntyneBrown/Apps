// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FreelanceProjectManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelanceProjectManager.Api.Features.Clients;

/// <summary>
/// Command to delete a client.
/// </summary>
public class DeleteClientCommand : IRequest<bool>
{
    /// <summary>
    /// Gets or sets the client ID.
    /// </summary>
    public Guid ClientId { get; set; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }
}

/// <summary>
/// Handler for deleting a client.
/// </summary>
public class DeleteClientHandler : IRequestHandler<DeleteClientCommand, bool>
{
    private readonly IFreelanceProjectManagerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteClientHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public DeleteClientHandler(IFreelanceProjectManagerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<bool> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        var client = await _context.Clients
            .Where(c => c.ClientId == request.ClientId && c.UserId == request.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (client == null)
        {
            return false;
        }

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
