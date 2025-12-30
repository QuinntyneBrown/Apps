// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConferenceEventManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConferenceEventManager.Api.Features.Contacts;

/// <summary>
/// Query to get all contacts.
/// </summary>
public class GetContacts
{
    /// <summary>
    /// Query to get all contacts, optionally filtered by event ID or user ID.
    /// </summary>
    public class Query : IRequest<List<ContactDto>>
    {
        public Guid? EventId { get; set; }
        public Guid? UserId { get; set; }
    }

    /// <summary>
    /// Handler for GetContacts query.
    /// </summary>
    public class Handler : IRequestHandler<Query, List<ContactDto>>
    {
        private readonly IConferenceEventManagerContext _context;

        public Handler(IConferenceEventManagerContext context)
        {
            _context = context;
        }

        public async Task<List<ContactDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _context.Contacts.AsQueryable();

            if (request.EventId.HasValue)
            {
                query = query.Where(c => c.EventId == request.EventId.Value);
            }

            if (request.UserId.HasValue)
            {
                query = query.Where(c => c.UserId == request.UserId.Value);
            }

            var contacts = await query
                .OrderBy(c => c.Name)
                .ToListAsync(cancellationToken);

            return contacts.Select(ContactDto.FromContact).ToList();
        }
    }
}
