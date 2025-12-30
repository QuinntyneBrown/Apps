// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DigitalLegacyPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DigitalLegacyPlanner.Api.Features.LegacyDocuments.Queries;

/// <summary>
/// Query to get a legacy document by ID.
/// </summary>
public class GetLegacyDocumentByIdQuery : IRequest<LegacyDocumentDto?>
{
    public Guid LegacyDocumentId { get; set; }
}

/// <summary>
/// Handler for GetLegacyDocumentByIdQuery.
/// </summary>
public class GetLegacyDocumentByIdQueryHandler : IRequestHandler<GetLegacyDocumentByIdQuery, LegacyDocumentDto?>
{
    private readonly IDigitalLegacyPlannerContext _context;

    public GetLegacyDocumentByIdQueryHandler(IDigitalLegacyPlannerContext context)
    {
        _context = context;
    }

    public async Task<LegacyDocumentDto?> Handle(GetLegacyDocumentByIdQuery request, CancellationToken cancellationToken)
    {
        var document = await _context.Documents
            .FirstOrDefaultAsync(d => d.LegacyDocumentId == request.LegacyDocumentId, cancellationToken);

        if (document == null)
        {
            return null;
        }

        return new LegacyDocumentDto
        {
            LegacyDocumentId = document.LegacyDocumentId,
            UserId = document.UserId,
            Title = document.Title,
            DocumentType = document.DocumentType,
            FilePath = document.FilePath,
            Description = document.Description,
            PhysicalLocation = document.PhysicalLocation,
            AccessGrantedTo = document.AccessGrantedTo,
            IsEncrypted = document.IsEncrypted,
            LastReviewedAt = document.LastReviewedAt,
            CreatedAt = document.CreatedAt
        };
    }
}
