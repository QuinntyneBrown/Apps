// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DigitalLegacyPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DigitalLegacyPlanner.Api.Features.LegacyDocuments.Commands;

/// <summary>
/// Command to update an existing legacy document.
/// </summary>
public class UpdateLegacyDocumentCommand : IRequest<LegacyDocumentDto?>
{
    public Guid LegacyDocumentId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string DocumentType { get; set; } = string.Empty;
    public string? FilePath { get; set; }
    public string? Description { get; set; }
    public string? PhysicalLocation { get; set; }
    public string? AccessGrantedTo { get; set; }
    public bool IsEncrypted { get; set; }
}

/// <summary>
/// Handler for UpdateLegacyDocumentCommand.
/// </summary>
public class UpdateLegacyDocumentCommandHandler : IRequestHandler<UpdateLegacyDocumentCommand, LegacyDocumentDto?>
{
    private readonly IDigitalLegacyPlannerContext _context;

    public UpdateLegacyDocumentCommandHandler(IDigitalLegacyPlannerContext context)
    {
        _context = context;
    }

    public async Task<LegacyDocumentDto?> Handle(UpdateLegacyDocumentCommand request, CancellationToken cancellationToken)
    {
        var document = await _context.Documents
            .FirstOrDefaultAsync(d => d.LegacyDocumentId == request.LegacyDocumentId, cancellationToken);

        if (document == null)
        {
            return null;
        }

        document.Title = request.Title;
        document.DocumentType = request.DocumentType;
        document.FilePath = request.FilePath;
        document.Description = request.Description;
        document.PhysicalLocation = request.PhysicalLocation;
        document.AccessGrantedTo = request.AccessGrantedTo;
        document.IsEncrypted = request.IsEncrypted;

        await _context.SaveChangesAsync(cancellationToken);

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
