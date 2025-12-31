// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace DailyJournalingApp.Core.Model.UserAggregate.Entities;

public class UserRole
{
    public Guid UserId { get; private set; }
    public Guid RoleId { get; private set; }
    public Guid TenantId { get; private set; }

    private UserRole() { }

    public UserRole(Guid tenantId, Guid userId, Guid roleId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("UserId cannot be empty.", nameof(userId));
        if (roleId == Guid.Empty)
            throw new ArgumentException("RoleId cannot be empty.", nameof(roleId));

        TenantId = tenantId;
        UserId = userId;
        RoleId = roleId;
    }
}
