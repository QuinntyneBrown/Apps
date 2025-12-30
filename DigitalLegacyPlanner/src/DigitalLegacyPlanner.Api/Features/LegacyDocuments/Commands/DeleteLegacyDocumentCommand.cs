// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DigitalLegacyPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DigitalLegacyPlanner.Api.Features.LegacyDocuments.Commands;

/// <summary>
/// Command to delete a legacy document.
/// </summary>
public class DeleteLegacyDocumentCommand : IRequest<bool>
{
    public Guid LegacyDocumentId { get; set; }
}

/// <summary>
/// Handler for DeleteLegacyDocumentCommand.
/// </summary>
public class DeleteLegacyDocumentCommandHandler : IRequestHandler<DeleteLegacyDocumentCommand, bool>
{
    private readonly IDigitalLegacyPlannerContext _context;

    public DeleteLegacyDocumentCommandHandler(IDigitalLegacyPlannerContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteLegacyDocumentCommand request, CancellationToken cancellationToken)
    {
        var document = await _context.Documents
            .FirstOrDefaultAsync(d => d.LegacyDocumentId == request.LegacyDocumentId, cancellationToken);

        if (document == null)
        {
            return false;
        }

        _context.Documents.Remove(document);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
