// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;

using ProfessionalNetworkCRM.Core.Models.UserAggregate;
using ProfessionalNetworkCRM.Core.Models.UserAggregate.Entities;
using ProfessionalNetworkCRM.Core.Models.OpportunityAggregate;
using ProfessionalNetworkCRM.Core.Models.IntroductionAggregate;
using ProfessionalNetworkCRM.Core.Models.ReferralAggregate;
namespace ProfessionalNetworkCRM.Core;

/// <summary>
/// Represents the persistence surface for the ProfessionalNetworkCRM system.
/// </summary>
public interface IProfessionalNetworkCRMContext
{
    /// <summary>
    /// Gets or sets the DbSet of contacts.
    /// </summary>
    DbSet<Contact> Contacts { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of interactions.
    /// </summary>
    DbSet<Interaction> Interactions { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of follow-ups.
    /// </summary>
    DbSet<FollowUp> FollowUps { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of opportunities.
    /// </summary>
    DbSet<Opportunity> Opportunities { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of introductions.
    /// </summary>
    DbSet<Introduction> Introductions { get; set; }

    /// <summary>
    /// Gets or sets the DbSet of referrals.
    /// </summary>
    DbSet<Referral> Referrals { get; set; }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    
    /// <summary>
    /// Gets the users.
    /// </summary>
    DbSet<User> Users { get; }

    /// <summary>
    /// Gets the roles.
    /// </summary>
    DbSet<Role> Roles { get; }

    /// <summary>
    /// Gets the user roles.
    /// </summary>
    DbSet<UserRole> UserRoles { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
