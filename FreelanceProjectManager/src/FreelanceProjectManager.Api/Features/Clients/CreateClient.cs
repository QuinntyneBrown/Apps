// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FreelanceProjectManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelanceProjectManager.Api.Features.Clients;

/// <summary>
/// Command to create a new client.
/// </summary>
public class CreateClientCommand : IRequest<ClientDto>
{
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
}

/// <summary>
/// Handler for creating a client.
/// </summary>
public class CreateClientHandler : IRequestHandler<CreateClientCommand, ClientDto>
{
    private readonly IFreelanceProjectManagerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateClientHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CreateClientHandler(IFreelanceProjectManagerContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<ClientDto> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var client = new Client
        {
            ClientId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            CompanyName = request.CompanyName,
            Email = request.Email,
            Phone = request.Phone,
            Address = request.Address,
            Website = request.Website,
            Notes = request.Notes,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.Clients.Add(client);
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
