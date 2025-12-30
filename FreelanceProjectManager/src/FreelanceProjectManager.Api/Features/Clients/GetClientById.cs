// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FreelanceProjectManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelanceProjectManager.Api.Features.Clients;

/// <summary>
/// Query to get a client by ID.
/// </summary>
public class GetClientByIdQuery : IRequest<ClientDto?>
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
/// Handler for getting a client by ID.
/// </summary>
public class GetClientByIdHandler : IRequestHandler<GetClientByIdQuery, ClientDto?>
{
    private readonly IFreelanceProjectManagerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetClientByIdHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetClientByIdHandler(IFreelanceProjectManagerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<ClientDto?> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        var client = await _context.Clients
            .Where(c => c.ClientId == request.ClientId && c.UserId == request.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (client == null)
        {
            return null;
        }

        return new ClientDto
        {
            ClientId = client.ClientId,
            UserId = client.UserId,
            Name = client.Name,
            CompanyName = client.CompanyName,
            Email = client.Email,
            Phone = client.Phone,
            Address = client.Address,
            Website = client.Website,
            Notes = client.Notes,
            IsActive = client.IsActive,
            CreatedAt = client.CreatedAt,
            UpdatedAt = client.UpdatedAt
        };
    }
}
