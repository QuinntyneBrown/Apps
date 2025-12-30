// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DigitalLegacyPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DigitalLegacyPlanner.Api.Features.TrustedContacts.Queries;

/// <summary>
/// Query to get all trusted contacts for a user.
/// </summary>
public class GetTrustedContactsQuery : IRequest<List<TrustedContactDto>>
{
    public Guid? UserId { get; set; }
}

/// <summary>
/// Handler for GetTrustedContactsQuery.
/// </summary>
public class GetTrustedContactsQueryHandler : IRequestHandler<GetTrustedContactsQuery, List<TrustedContactDto>>
{
    private readonly IDigitalLegacyPlannerContext _context;

    public GetTrustedContactsQueryHandler(IDigitalLegacyPlannerContext context)
    {
        _context = context;
    }

    public async Task<List<TrustedContactDto>> Handle(GetTrustedContactsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Contacts.AsQueryable();

        if (request.UserId.HasValue)
        {
            query = query.Where(c => c.UserId == request.UserId.Value);
        }

        var contacts = await query.ToListAsync(cancellationToken);

        return contacts.Select(contact => new TrustedContactDto
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
        }).ToList();
    }
}
