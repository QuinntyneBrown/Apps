// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace FamilyCalendarEventPlanner.Core.Model.UserAggregate.Entities;

public class Role
{
    
    public Guid RoleId { get; set; }
    public string Name { get; set; }

    private Role()
    {

    }

    public Role(Guid roleId, string name)
    {
        RoleId = roleId;
        Name = name;
    }
}
    