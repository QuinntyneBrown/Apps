// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Security.Claims;

namespace FamilyCalendarEventPlanner.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(string userId, string userName, string email, IEnumerable<string> roles);
    ClaimsPrincipal? ValidateToken(string token);
}
