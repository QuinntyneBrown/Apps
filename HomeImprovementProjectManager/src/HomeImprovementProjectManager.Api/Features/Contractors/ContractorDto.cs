// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeImprovementProjectManager.Core;

namespace HomeImprovementProjectManager.Api;

/// <summary>
/// Data transfer object for Contractor.
/// </summary>
public record ContractorDto
{
    /// <summary>
    /// Gets or sets the contractor ID.
    /// </summary>
    public Guid ContractorId { get; init; }

    /// <summary>
    /// Gets or sets the project ID.
    /// </summary>
    public Guid? ProjectId { get; init; }

    /// <summary>
    /// Gets or sets the contractor name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the trade.
    /// </summary>
    public string? Trade { get; init; }

    /// <summary>
    /// Gets or sets the phone number.
    /// </summary>
    public string? PhoneNumber { get; init; }

    /// <summary>
    /// Gets or sets the email.
    /// </summary>
    public string? Email { get; init; }

    /// <summary>
    /// Gets or sets the rating.
    /// </summary>
    public int? Rating { get; init; }

    /// <summary>
    /// Gets or sets the created timestamp.
    /// </summary>
    public DateTime CreatedAt { get; init; }
}

/// <summary>
/// Extension methods for Contractor.
/// </summary>
public static class ContractorExtensions
{
    /// <summary>
    /// Converts a Contractor to a DTO.
    /// </summary>
    /// <param name="contractor">The contractor.</param>
    /// <returns>The DTO.</returns>
    public static ContractorDto ToDto(this Contractor contractor)
    {
        return new ContractorDto
        {
            ContractorId = contractor.ContractorId,
            ProjectId = contractor.ProjectId,
            Name = contractor.Name,
            Trade = contractor.Trade,
            PhoneNumber = contractor.PhoneNumber,
            Email = contractor.Email,
            Rating = contractor.Rating,
            CreatedAt = contractor.CreatedAt,
        };
    }
}
