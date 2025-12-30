// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DigitalLegacyPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DigitalLegacyPlanner.Api.Features.TrustedContacts.Commands;

/// <summary>
/// Command to delete a trusted contact.
/// </summary>
public class DeleteTrustedContactCommand : IRequest<bool>
{
    public Guid TrustedContactId { get; set; }
}

/// <summary>
/// Handler for DeleteTrustedContactCommand.
/// </summary>
public class DeleteTrustedContactCommandHandler : IRequestHandler<DeleteTrustedContactCommand, bool>
{
    private readonly IDigitalLegacyPlannerContext _context;

    public DeleteTrustedContactCommandHandler(IDigitalLegacyPlannerContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteTrustedContactCommand request, CancellationToken cancellationToken)
    {
        var contact = await _context.Contacts
            .FirstOrDefaultAsync(c => c.TrustedContactId == request.TrustedContactId, cancellationToken);

        if (contact == null)
        {
            return false;
        }

        _context.Contacts.Remove(contact);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
