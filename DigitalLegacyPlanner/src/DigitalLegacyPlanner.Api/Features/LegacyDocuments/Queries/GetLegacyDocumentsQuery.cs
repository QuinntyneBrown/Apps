// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DigitalLegacyPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DigitalLegacyPlanner.Api.Features.LegacyDocuments.Queries;

/// <summary>
/// Query to get all legacy documents for a user.
/// </summary>
public class GetLegacyDocumentsQuery : IRequest<List<LegacyDocumentDto>>
{
    public Guid? UserId { get; set; }
    public string? DocumentType { get; set; }
    public bool? NeedsReview { get; set; }
}

/// <summary>
/// Handler for GetLegacyDocumentsQuery.
/// </summary>
public class GetLegacyDocumentsQueryHandler : IRequestHandler<GetLegacyDocumentsQuery, List<LegacyDocumentDto>>
{
    private readonly IDigitalLegacyPlannerContext _context;

    public GetLegacyDocumentsQueryHandler(IDigitalLegacyPlannerContext context)
    {
        _context = context;
    }

    public async Task<List<LegacyDocumentDto>> Handle(GetLegacyDocumentsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Documents.AsQueryable();

        if (request.UserId.HasValue)
        {
            query = query.Where(d => d.UserId == request.UserId.Value);
        }

        if (!string.IsNullOrEmpty(request.DocumentType))
        {
            query = query.Where(d => d.DocumentType == request.DocumentType);
        }

        var documents = await query.ToListAsync(cancellationToken);

        if (request.NeedsReview.HasValue && request.NeedsReview.Value)
        {
            documents = documents.Where(d => d.NeedsReview()).ToList();
        }

        return documents.Select(document => new LegacyDocumentDto
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
        }).ToList();
    }
}
