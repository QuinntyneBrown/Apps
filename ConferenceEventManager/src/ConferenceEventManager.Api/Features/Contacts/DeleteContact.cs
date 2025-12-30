// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConferenceEventManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConferenceEventManager.Api.Features.Contacts;

/// <summary>
/// Command to delete a contact.
/// </summary>
public class DeleteContact
{
    /// <summary>
    /// Command to delete a contact.
    /// </summary>
    public class Command : IRequest<Unit>
    {
        public Guid ContactId { get; set; }
    }

    /// <summary>
    /// Handler for DeleteContact command.
    /// </summary>
    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IConferenceEventManagerContext _context;

        public Handler(IConferenceEventManagerContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var contact = await _context.Contacts
                .FirstOrDefaultAsync(c => c.ContactId == request.ContactId, cancellationToken)
                ?? throw new KeyNotFoundException($"Contact with ID {request.ContactId} not found.");

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
