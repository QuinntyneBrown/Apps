// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DigitalLegacyPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DigitalLegacyPlanner.Api.Features.TrustedContacts.Queries;

/// <summary>
/// Query to get a trusted contact by ID.
/// </summary>
public class GetTrustedContactByIdQuery : IRequest<TrustedContactDto?>
{
    public Guid TrustedContactId { get; set; }
}

/// <summary>
/// Handler for GetTrustedContactByIdQuery.
/// </summary>
public class GetTrustedContactByIdQueryHandler : IRequestHandler<GetTrustedContactByIdQuery, TrustedContactDto?>
{
    private readonly IDigitalLegacyPlannerContext _context;

    public GetTrustedContactByIdQueryHandler(IDigitalLegacyPlannerContext context)
    {
        _context = context;
    }

    public async Task<TrustedContactDto?> Handle(GetTrustedContactByIdQuery request, CancellationToken cancellationToken)
    {
        var contact = await _context.Contacts
            .FirstOrDefaultAsync(c => c.TrustedContactId == request.TrustedContactId, cancellationToken);

        if (contact == null)
        {
            return null;
        }

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
