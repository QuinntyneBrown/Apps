// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DigitalLegacyPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DigitalLegacyPlanner.Api.Features.TrustedContacts.Commands;

/// <summary>
/// Command to create a new trusted contact.
/// </summary>
public class CreateTrustedContactCommand : IRequest<TrustedContactDto>
{
    public Guid UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Relationship { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string? Role { get; set; }
    public bool IsPrimaryContact { get; set; }
    public string? Notes { get; set; }
}

/// <summary>
/// Handler for CreateTrustedContactCommand.
/// </summary>
public class CreateTrustedContactCommandHandler : IRequestHandler<CreateTrustedContactCommand, TrustedContactDto>
{
    private readonly IDigitalLegacyPlannerContext _context;

    public CreateTrustedContactCommandHandler(IDigitalLegacyPlannerContext context)
    {
        _context = context;
    }

    public async Task<TrustedContactDto> Handle(CreateTrustedContactCommand request, CancellationToken cancellationToken)
    {
        var contact = new TrustedContact
        {
            TrustedContactId = Guid.NewGuid(),
            UserId = request.UserId,
            FullName = request.FullName,
            Relationship = request.Relationship,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Role = request.Role,
            IsPrimaryContact = request.IsPrimaryContact,
            Notes = request.Notes,
            IsNotified = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.Contacts.Add(contact);
        await _context.SaveChangesAsync(cancellationToken);

        return new TrustedContactDto
        {
            TrustedContactId = contact.TrustedContactId,
            UserId = contact.UserId,
            FullName = contact.FullName,
            Relationship = contact.Relationship,
            Email = contact.Email,
            PhoneNumber = contact.PhoneNumber,
            Role = contact.Role,
            IsPrimaryContact = contact.IsPrimaryContact,
            IsNotified = contact.IsNotified,
            Notes = contact.Notes,
            CreatedAt = contact.CreatedAt
        };
    }
}
