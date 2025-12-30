// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace DigitalLegacyPlanner.Api.Features.LegacyDocuments;

/// <summary>
/// Data transfer object for LegacyDocument.
/// </summary>
public class LegacyDocumentDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the document.
    /// </summary>
    public Guid LegacyDocumentId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this document.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the document title or name.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the document type (will, power of attorney, etc.).
    /// </summary>
    public string DocumentType { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the file path or storage location.
    /// </summary>
    public string? FilePath { get; set; }

    /// <summary>
    /// Gets or sets the description or summary.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the location of physical copy.
    /// </summary>
    public string? PhysicalLocation { get; set; }

    /// <summary>
    /// Gets or sets who should have access to this document.
    /// </summary>
    public string? AccessGrantedTo { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the document is encrypted.
    /// </summary>
    public bool IsEncrypted { get; set; }

    /// <summary>
    /// Gets or sets the last review date.
    /// </summary>
    public DateTime? LastReviewedAt { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
