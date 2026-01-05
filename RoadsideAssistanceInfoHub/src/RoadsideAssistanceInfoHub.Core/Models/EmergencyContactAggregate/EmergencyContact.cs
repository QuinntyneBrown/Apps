// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RoadsideAssistanceInfoHub.Core;

/// <summary>
/// Represents an emergency contact for roadside assistance.
/// </summary>
public class EmergencyContact
{
    /// <summary>
    /// Gets or sets the unique identifier for the emergency contact.
    /// </summary>
    public Guid EmergencyContactId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the contact name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the relationship (e.g., "Spouse", "Friend", "Tow Service").
    /// </summary>
    public string? Relationship { get; set; }

    /// <summary>
    /// Gets or sets the primary phone number.
    /// </summary>
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the alternate phone number.
    /// </summary>
    public string? AlternatePhone { get; set; }

    /// <summary>
    /// Gets or sets the email address.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the address.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Gets or sets whether this is a primary contact.
    /// </summary>
    public bool IsPrimaryContact { get; set; }

    /// <summary>
    /// Gets or sets the contact type (e.g., "Personal", "Tow Service", "Mechanic").
    /// </summary>
    public string? ContactType { get; set; }

    /// <summary>
    /// Gets or sets the service area for service providers.
    /// </summary>
    public string? ServiceArea { get; set; }

    /// <summary>
    /// Gets or sets additional notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets whether this contact is active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Marks this contact as the primary contact.
    /// </summary>
    public void SetAsPrimary()
    {
        IsPrimaryContact = true;
    }

    /// <summary>
    /// Removes primary status from this contact.
    /// </summary>
    public void RemovePrimary()
    {
        IsPrimaryContact = false;
    }

    /// <summary>
    /// Deactivates the contact.
    /// </summary>
    public void Deactivate()
    {
        IsActive = false;
    }

    /// <summary>
    /// Reactivates the contact.
    /// </summary>
    public void Reactivate()
    {
        IsActive = true;
    }

    /// <summary>
    /// Updates the phone number.
    /// </summary>
    /// <param name="newPhoneNumber">The new phone number.</param>
    public void UpdatePhoneNumber(string newPhoneNumber)
    {
        PhoneNumber = newPhoneNumber;
    }
}
