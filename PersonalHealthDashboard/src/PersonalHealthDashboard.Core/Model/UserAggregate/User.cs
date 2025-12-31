// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using PersonalHealthDashboard.Core.Model.UserAggregate.Entities;

namespace PersonalHealthDashboard.Core.Model.UserAggregate;

public class User
{
    private readonly List<UserRole> _userRoles = new();

    public Guid UserId { get; private set; }
    public Guid TenantId { get; private set; }
    public string UserName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;
    public byte[] Salt { get; private set; } = Array.Empty<byte>();
    public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();

    private User() { }

    public User(Guid tenantId, string userName, string email, string hashedPassword, byte[] salt)
    {
        if (string.IsNullOrWhiteSpace(userName))
            throw new ArgumentException("UserName cannot be empty.", nameof(userName));
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty.", nameof(email));
        if (string.IsNullOrWhiteSpace(hashedPassword))
            throw new ArgumentException("Password cannot be empty.", nameof(hashedPassword));
        if (salt == null || salt.Length == 0)
            throw new ArgumentException("Salt cannot be empty.", nameof(salt));

        UserId = Guid.NewGuid();
        TenantId = tenantId;
        UserName = userName;
        Email = email;
        Password = hashedPassword;
        Salt = salt;
    }

    public void UpdateProfile(string? userName = null, string? email = null)
    {
        if (userName != null)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("UserName cannot be empty.", nameof(userName));
            UserName = userName;
        }

        if (email != null)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty.", nameof(email));
            Email = email;
        }
    }

    public void SetPassword(string hashedPassword, byte[] salt)
    {
        if (string.IsNullOrWhiteSpace(hashedPassword))
            throw new ArgumentException("Password cannot be empty.", nameof(hashedPassword));
        if (salt == null || salt.Length == 0)
            throw new ArgumentException("Salt cannot be empty.", nameof(salt));

        Password = hashedPassword;
        Salt = salt;
    }

    public void AddRole(Role role)
    {
        if (role == null)
            throw new ArgumentNullException(nameof(role));

        if (_userRoles.Any(ur => ur.RoleId == role.RoleId))
            return;

        _userRoles.Add(new UserRole(TenantId, UserId, role.RoleId));
    }

    public void RemoveRole(Guid roleId)
    {
        var userRole = _userRoles.FirstOrDefault(ur => ur.RoleId == roleId);
        if (userRole != null)
        {
            _userRoles.Remove(userRole);
        }
    }

    public bool HasRole(string roleName, IEnumerable<Role> allRoles)
    {
        var roleIds = _userRoles.Select(ur => ur.RoleId).ToHashSet();
        return allRoles.Any(r => roleIds.Contains(r.RoleId) &&
            r.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase));
    }
}
