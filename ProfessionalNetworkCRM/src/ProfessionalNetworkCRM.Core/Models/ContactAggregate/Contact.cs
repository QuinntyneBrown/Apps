// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ProfessionalNetworkCRM.Core;

/// <summary>
/// Represents a professional contact.
/// </summary>
public class Contact
{
    /// <summary>
    /// Gets or sets the unique identifier for the contact.
    /// </summary>
    public Guid ContactId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this contact.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the contact's first name.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the contact's last name.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the contact type.
    /// </summary>
    public ContactType ContactType { get; set; }

    /// <summary>
    /// Gets or sets the company or organization.
    /// </summary>
    public string? Company { get; set; }

    /// <summary>
    /// Gets or sets the job title.
    /// </summary>
    public string? JobTitle { get; set; }

    /// <summary>
    /// Gets or sets the email address.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the phone number.
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Gets or sets the LinkedIn profile URL.
    /// </summary>
    public string? LinkedInUrl { get; set; }

    /// <summary>
    /// Gets or sets the location (city, state, country).
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Gets or sets optional notes about this contact.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets tags for categorization.
    /// </summary>
    public List<string> Tags { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets the date when the contact was met or added.
    /// </summary>
    public DateTime DateMet { get; set; }

    /// <summary>
    /// Gets or sets the date of last interaction.
    /// </summary>
    public DateTime? LastContactedDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this contact is a priority.
    /// </summary>
    public bool IsPriority { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the collection of interactions with this contact.
    /// </summary>
    public ICollection<Interaction> Interactions { get; set; } = new List<Interaction>();

    /// <summary>
    /// Gets or sets the collection of follow-ups for this contact.
    /// </summary>
    public ICollection<FollowUp> FollowUps { get; set; } = new List<FollowUp>();

    /// <summary>
    /// Gets the full name of the contact.
    /// </summary>
    public string FullName => $"{FirstName} {LastName}".Trim();

    /// <summary>
    /// Updates the last contacted date.
    /// </summary>
    /// <param name="date">The date of last contact.</param>
    public void UpdateLastContactedDate(DateTime date)
    {
        LastContactedDate = date;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Toggles the priority status of this contact.
    /// </summary>
    public void TogglePriority()
    {
        IsPriority = !IsPriority;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Adds a tag to this contact.
    /// </summary>
    /// <param name="tag">The tag to add.</param>
    public void AddTag(string tag)
    {
        if (!Tags.Contains(tag, StringComparer.OrdinalIgnoreCase))
        {
            Tags.Add(tag);
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
