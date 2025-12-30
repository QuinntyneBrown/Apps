// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DigitalLegacyPlanner.Core;
using MediatR;

namespace DigitalLegacyPlanner.Api.Features.LegacyDocuments.Commands;

/// <summary>
/// Command to create a new legacy document.
/// </summary>
public class CreateLegacyDocumentCommand : IRequest<LegacyDocumentDto>
{
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string DocumentType { get; set; } = string.Empty;
    public string? FilePath { get; set; }
    public string? Description { get; set; }
    public string? PhysicalLocation { get; set; }
    public string? AccessGrantedTo { get; set; }
    public bool IsEncrypted { get; set; }
}

/// <summary>
/// Handler for CreateLegacyDocumentCommand.
/// </summary>
public class CreateLegacyDocumentCommandHandler : IRequestHandler<CreateLegacyDocumentCommand, LegacyDocumentDto>
{
    private readonly IDigitalLegacyPlannerContext _context;

    public CreateLegacyDocumentCommandHandler(IDigitalLegacyPlannerContext context)
    {
        _context = context;
    }

    public async Task<LegacyDocumentDto> Handle(CreateLegacyDocumentCommand request, CancellationToken cancellationToken)
    {
        var document = new LegacyDocument
        {
            LegacyDocumentId = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            DocumentType = request.DocumentType,
            FilePath = request.FilePath,
            Description = request.Description,
            PhysicalLocation = request.PhysicalLocation,
            AccessGrantedTo = request.AccessGrantedTo,
            IsEncrypted = request.IsEncrypted,
            CreatedAt = DateTime.UtcNow
        };

        _context.Documents.Add(document);
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
