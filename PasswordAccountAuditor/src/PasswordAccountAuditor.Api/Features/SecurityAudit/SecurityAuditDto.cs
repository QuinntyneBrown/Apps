// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PasswordAccountAuditor.Core;

namespace PasswordAccountAuditor.Api.Features.SecurityAudit;

/// <summary>
/// Data transfer object for SecurityAudit.
/// </summary>
public record SecurityAuditDto
{
    public Guid SecurityAuditId { get; init; }
    public Guid AccountId { get; init; }
    public AuditType AuditType { get; init; }
    public AuditStatus Status { get; init; }
    public DateTime AuditDate { get; init; }
    public string? Findings { get; init; }
    public string? Recommendations { get; init; }
    public int SecurityScore { get; init; }
    public string? Notes { get; init; }
}

/// <summary>
/// Extension methods for mapping SecurityAudit to SecurityAuditDto.
/// </summary>
public static class SecurityAuditExtensions
{
    /// <summary>
    /// Converts a SecurityAudit entity to a SecurityAuditDto.
    /// </summary>
    /// <param name="securityAudit">The security audit entity.</param>
    /// <returns>The security audit DTO.</returns>
    public static SecurityAuditDto ToDto(this Core.SecurityAudit securityAudit)
    {
        return new SecurityAuditDto
        {
            SecurityAuditId = securityAudit.SecurityAuditId,
            AccountId = securityAudit.AccountId,
            AuditType = securityAudit.AuditType,
            Status = securityAudit.Status,
            AuditDate = securityAudit.AuditDate,
            Findings = securityAudit.Findings,
            Recommendations = securityAudit.Recommendations,
            SecurityScore = securityAudit.SecurityScore,
            Notes = securityAudit.Notes
        };
    }
}
