// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FreelanceProjectManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelanceProjectManager.Api.Features.Clients;

/// <summary>
/// Command to update a client.
/// </summary>
public class UpdateClientCommand : IRequest<ClientDto?>
{
    /// <summary>
    /// Gets or sets the client ID.
    /// </summary>
    public Guid ClientId { get; set; }

    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the client name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the company name.
    /// </summary>
    public string? CompanyName { get; set; }

    /// <summary>
    /// Gets or sets the email.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the phone.
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Gets or sets the address.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Gets or sets the website.
    /// </summary>
    public string? Website { get; set; }

    /// <summary>
    /// Gets or sets notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this is an active client.
    /// </summary>
    public bool IsActive { get; set; }
}

/// <summary>
/// Handler for updating a client.
/// </summary>
public class UpdateClientHandler : IRequestHandler<UpdateClientCommand, ClientDto?>
{
    private readonly IFreelanceProjectManagerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateClientHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public UpdateClientHandler(IFreelanceProjectManagerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<ClientDto?> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var client = await _context.Clients
            .Where(c => c.ClientId == request.ClientId && c.UserId == request.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (client == null)
        {
            return null;
        }

        client.Name = request.Name;
        client.CompanyName = request.CompanyName;
        client.Email = request.Email;
        client.Phone = request.Phone;
        client.Address = request.Address;
        client.Website = request.Website;
        client.Notes = request.Notes;
        client.IsActive = request.IsActive;
        client.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

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
