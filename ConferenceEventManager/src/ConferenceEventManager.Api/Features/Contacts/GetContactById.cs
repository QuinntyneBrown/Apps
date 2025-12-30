// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConferenceEventManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConferenceEventManager.Api.Features.Contacts;

/// <summary>
/// Query to get a contact by ID.
/// </summary>
public class GetContactById
{
    /// <summary>
    /// Query to get a contact by ID.
    /// </summary>
    public class Query : IRequest<ContactDto>
    {
        public Guid ContactId { get; set; }
    }

    /// <summary>
    /// Handler for GetContactById query.
    /// </summary>
    public class Handler : IRequestHandler<Query, ContactDto>
    {
        private readonly IConferenceEventManagerContext _context;

        public Handler(IConferenceEventManagerContext context)
        {
            _context = context;
        }

        public async Task<ContactDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var contact = await _context.Contacts
                .FirstOrDefaultAsync(c => c.ContactId == request.ContactId, cancellationToken)
                ?? throw new KeyNotFoundException($"Contact with ID {request.ContactId} not found.");

            return ContactDto.FromContact(contact);
        }
    }
}
