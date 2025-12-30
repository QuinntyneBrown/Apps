// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DigitalLegacyPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DigitalLegacyPlanner.Api.Features.TrustedContacts.Commands;

/// <summary>
/// Command to update an existing trusted contact.
/// </summary>
public class UpdateTrustedContactCommand : IRequest<TrustedContactDto?>
{
    public Guid TrustedContactId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Relationship { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string? Role { get; set; }
    public bool IsPrimaryContact { get; set; }
    public string? Notes { get; set; }
}

/// <summary>
/// Handler for UpdateTrustedContactCommand.
/// </summary>
public class UpdateTrustedContactCommandHandler : IRequestHandler<UpdateTrustedContactCommand, TrustedContactDto?>
{
    private readonly IDigitalLegacyPlannerContext _context;

    public UpdateTrustedContactCommandHandler(IDigitalLegacyPlannerContext context)
    {
        _context = context;
    }

    public async Task<TrustedContactDto?> Handle(UpdateTrustedContactCommand request, CancellationToken cancellationToken)
    {
        var contact = await _context.Contacts
            .FirstOrDefaultAsync(c => c.TrustedContactId == request.TrustedContactId, cancellationToken);

        if (contact == null)
        {
            return null;
        }

        contact.FullName = request.FullName;
        contact.Relationship = request.Relationship;
        contact.Email = request.Email;
        contact.PhoneNumber = request.PhoneNumber;
        contact.Role = request.Role;
        contact.IsPrimaryContact = request.IsPrimaryContact;
        contact.Notes = request.Notes;

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
