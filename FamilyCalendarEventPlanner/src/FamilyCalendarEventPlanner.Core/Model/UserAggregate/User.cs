// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using FamilyCalendarEventPlanner.Core.Model.UserAggregate.Entities;

namespace FamilyCalendarEventPlanner.Core.Model.UserAggregate;

/// <summary>
/// Aggregate root representing a user in the FamilyCalendarEventPlanner system.
/// </summary>
public class User
{
    public Guid UserId { get; private set; }
    public string UserName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public byte[] Salt { get; private set; }
    public List<Role> Roles { get; private set; } = new();    


    private User() { }

    public User(Guid userId, string userName, string email)
    {
        if (string.IsNullOrWhiteSpace(userName))
            throw new ArgumentException("UserName cannot be empty.", nameof(userName));
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty.", nameof(email));

        UserId = userId;
        UserName = userName;
        Email = email;
        Password = string.Empty;
        Salt = [];
        Roles = [];
    }

    public void UpdateProfile(string userName, string email)
    {
        if (string.IsNullOrWhiteSpace(userName))
            throw new ArgumentException("UserName cannot be empty.", nameof(userName));
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty.", nameof(email));
        UserName = userName;
        Email = email;
    }
}
