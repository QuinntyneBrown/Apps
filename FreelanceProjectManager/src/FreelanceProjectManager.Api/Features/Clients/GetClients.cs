// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FreelanceProjectManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelanceProjectManager.Api.Features.Clients;

/// <summary>
/// Query to get all clients for a user.
/// </summary>
public class GetClientsQuery : IRequest<List<ClientDto>>
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }
}

/// <summary>
/// Handler for getting clients.
/// </summary>
public class GetClientsHandler : IRequestHandler<GetClientsQuery, List<ClientDto>>
{
    private readonly IFreelanceProjectManagerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetClientsHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public GetClientsHandler(IFreelanceProjectManagerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<List<ClientDto>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Clients
            .Where(c => c.UserId == request.UserId)
            .Select(c => new ClientDto
            {
                ClientId = c.ClientId,
                UserId = c.UserId,
                Name = c.Name,
                CompanyName = c.CompanyName,
                Email = c.Email,
                Phone = c.Phone,
                Address = c.Address,
                Website = c.Website,
                Notes = c.Notes,
                IsActive = c.IsActive,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
