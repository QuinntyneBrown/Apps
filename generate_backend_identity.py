#!/usr/bin/env python3
"""Script to generate identity backend implementation for all apps."""

import os
import re

APPS_DIR = "/home/user/Apps"
SKIP_DIRS = ["FamilyCalendarEventPlanner", "AnniversaryBirthdayReminder", ".git", ".claude"]

def get_apps():
    apps = []
    for item in os.listdir(APPS_DIR):
        if os.path.isdir(os.path.join(APPS_DIR, item)) and item not in SKIP_DIRS:
            # Only include actual apps (those with src directory)
            if os.path.isdir(os.path.join(APPS_DIR, item, "src")):
                apps.append(item)
    return sorted(apps)

def get_email_domain(app_name):
    s = re.sub('(.)([A-Z][a-z]+)', r'\1-\2', app_name)
    return re.sub('([a-z0-9])([A-Z])', r'\1-\2', s).lower()

def create_user_cs(app_name, app_dir):
    content = f'''// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using {app_name}.Core.Model.UserAggregate.Entities;

namespace {app_name}.Core.Model.UserAggregate;

public class User
{{
    private readonly List<UserRole> _userRoles = new();

    public Guid UserId {{ get; private set; }}
    public Guid TenantId {{ get; private set; }}
    public string UserName {{ get; private set; }} = string.Empty;
    public string Email {{ get; private set; }} = string.Empty;
    public string Password {{ get; private set; }} = string.Empty;
    public byte[] Salt {{ get; private set; }} = Array.Empty<byte>();
    public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();

    private User() {{ }}

    public User(Guid tenantId, string userName, string email, string hashedPassword, byte[] salt)
    {{
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
    }}

    public void UpdateProfile(string? userName = null, string? email = null)
    {{
        if (userName != null)
        {{
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("UserName cannot be empty.", nameof(userName));
            UserName = userName;
        }}

        if (email != null)
        {{
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty.", nameof(email));
            Email = email;
        }}
    }}

    public void SetPassword(string hashedPassword, byte[] salt)
    {{
        if (string.IsNullOrWhiteSpace(hashedPassword))
            throw new ArgumentException("Password cannot be empty.", nameof(hashedPassword));
        if (salt == null || salt.Length == 0)
            throw new ArgumentException("Salt cannot be empty.", nameof(salt));

        Password = hashedPassword;
        Salt = salt;
    }}

    public void AddRole(Role role)
    {{
        if (role == null)
            throw new ArgumentNullException(nameof(role));

        if (_userRoles.Any(ur => ur.RoleId == role.RoleId))
            return;

        _userRoles.Add(new UserRole(TenantId, UserId, role.RoleId));
    }}

    public void RemoveRole(Guid roleId)
    {{
        var userRole = _userRoles.FirstOrDefault(ur => ur.RoleId == roleId);
        if (userRole != null)
        {{
            _userRoles.Remove(userRole);
        }}
    }}

    public bool HasRole(string roleName, IEnumerable<Role> allRoles)
    {{
        var roleIds = _userRoles.Select(ur => ur.RoleId).ToHashSet();
        return allRoles.Any(r => roleIds.Contains(r.RoleId) &&
            r.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase));
    }}
}}
'''
    dir_path = os.path.join(app_dir, "src", f"{app_name}.Core", "Model", "UserAggregate")
    os.makedirs(dir_path, exist_ok=True)
    with open(os.path.join(dir_path, "User.cs"), "w") as f:
        f.write(content)

def create_role_cs(app_name, app_dir):
    content = f'''// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace {app_name}.Core.Model.UserAggregate.Entities;

public class Role
{{
    public Guid RoleId {{ get; private set; }}
    public Guid TenantId {{ get; private set; }}
    public string Name {{ get; private set; }} = string.Empty;

    private Role() {{ }}

    public Role(Guid tenantId, string name)
    {{
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Role name cannot be empty.", nameof(name));

        RoleId = Guid.NewGuid();
        TenantId = tenantId;
        Name = name;
    }}

    public void UpdateName(string name)
    {{
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Role name cannot be empty.", nameof(name));

        Name = name;
    }}
}}
'''
    dir_path = os.path.join(app_dir, "src", f"{app_name}.Core", "Model", "UserAggregate", "Entities")
    os.makedirs(dir_path, exist_ok=True)
    with open(os.path.join(dir_path, "Role.cs"), "w") as f:
        f.write(content)

def create_userrole_cs(app_name, app_dir):
    content = f'''// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace {app_name}.Core.Model.UserAggregate.Entities;

public class UserRole
{{
    public Guid UserId {{ get; private set; }}
    public Guid RoleId {{ get; private set; }}
    public Guid TenantId {{ get; private set; }}

    private UserRole() {{ }}

    public UserRole(Guid tenantId, Guid userId, Guid roleId)
    {{
        if (userId == Guid.Empty)
            throw new ArgumentException("UserId cannot be empty.", nameof(userId));
        if (roleId == Guid.Empty)
            throw new ArgumentException("RoleId cannot be empty.", nameof(roleId));

        TenantId = tenantId;
        UserId = userId;
        RoleId = roleId;
    }}
}}
'''
    dir_path = os.path.join(app_dir, "src", f"{app_name}.Core", "Model", "UserAggregate", "Entities")
    os.makedirs(dir_path, exist_ok=True)
    with open(os.path.join(dir_path, "UserRole.cs"), "w") as f:
        f.write(content)

def create_ipassword_hasher_cs(app_name, app_dir):
    content = f'''namespace {app_name}.Core.Services;

public interface IPasswordHasher
{{
    (string HashedPassword, byte[] Salt) HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword, byte[] salt);
}}
'''
    dir_path = os.path.join(app_dir, "src", f"{app_name}.Core", "Services")
    os.makedirs(dir_path, exist_ok=True)
    with open(os.path.join(dir_path, "IPasswordHasher.cs"), "w") as f:
        f.write(content)

def create_password_hasher_cs(app_name, app_dir):
    content = f'''using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace {app_name}.Core.Services;

public class PasswordHasher : IPasswordHasher
{{
    private const int SaltSize = 32;
    private const int HashSize = 32;
    private const int Iterations = 100000;

    public (string HashedPassword, byte[] Salt) HashPassword(string password)
    {{
        if (string.IsNullOrEmpty(password))
            throw new ArgumentException("Password cannot be empty.", nameof(password));

        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        string hashedPassword = HashPasswordWithSalt(password, salt);

        return (hashedPassword, salt);
    }}

    public bool VerifyPassword(string password, string hashedPassword, byte[] salt)
    {{
        if (string.IsNullOrEmpty(password))
            return false;

        if (string.IsNullOrEmpty(hashedPassword) || salt == null || salt.Length == 0)
            return false;

        string computedHash = HashPasswordWithSalt(password, salt);
        return CryptographicOperations.FixedTimeEquals(
            Convert.FromBase64String(computedHash),
            Convert.FromBase64String(hashedPassword));
    }}

    private static string HashPasswordWithSalt(string password, byte[] salt)
    {{
        byte[] hash = KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: Iterations,
            numBytesRequested: HashSize);

        return Convert.ToBase64String(hash);
    }}
}}
'''
    dir_path = os.path.join(app_dir, "src", f"{app_name}.Core", "Services")
    os.makedirs(dir_path, exist_ok=True)
    with open(os.path.join(dir_path, "PasswordHasher.cs"), "w") as f:
        f.write(content)

def create_ijwt_token_service_cs(app_name, app_dir):
    content = f'''using {app_name}.Core.Model.UserAggregate;
using {app_name}.Core.Model.UserAggregate.Entities;

namespace {app_name}.Core.Services;

public interface IJwtTokenService
{{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}}
'''
    dir_path = os.path.join(app_dir, "src", f"{app_name}.Core", "Services")
    os.makedirs(dir_path, exist_ok=True)
    with open(os.path.join(dir_path, "IJwtTokenService.cs"), "w") as f:
        f.write(content)

def create_jwt_token_service_cs(app_name, app_dir):
    content = f'''using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using {app_name}.Core.Model.UserAggregate;
using {app_name}.Core.Model.UserAggregate.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace {app_name}.Core.Services;

public class JwtTokenService : IJwtTokenService
{{
    private readonly IConfiguration _configuration;
    private readonly int _expiresInHours;

    public JwtTokenService(IConfiguration configuration)
    {{
        _configuration = configuration;
        _expiresInHours = configuration.GetValue<int>("Jwt:ExpiresInHours", 24);
    }}

    public string GenerateToken(User user, IEnumerable<Role> roles)
    {{
        var key = _configuration["Jwt:Key"]
            ?? throw new InvalidOperationException("JWT Key is not configured.");
        var issuer = _configuration["Jwt:Issuer"]
            ?? "{app_name}";
        var audience = _configuration["Jwt:Audience"]
            ?? "{app_name}.Api";

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {{
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
        }};

        foreach (var role in roles)
        {{
            claims.Add(new Claim(ClaimTypes.Role, role.Name));
        }}

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: GetTokenExpiration(),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }}

    public DateTime GetTokenExpiration()
    {{
        return DateTime.UtcNow.AddHours(_expiresInHours);
    }}
}}
'''
    dir_path = os.path.join(app_dir, "src", f"{app_name}.Core", "Services")
    os.makedirs(dir_path, exist_ok=True)
    with open(os.path.join(dir_path, "JwtTokenService.cs"), "w") as f:
        f.write(content)

def create_constants_cs(app_name, app_dir):
    content = f'''// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;

namespace {app_name}.Core;

public static class Constants
{{
    public static Guid DefaultTenantId => new Guid("3e802e65-916e-4f2c-8068-abdd3b93dc2c");
}}
'''
    dir_path = os.path.join(app_dir, "src", f"{app_name}.Core")
    with open(os.path.join(dir_path, "Constants.cs"), "w") as f:
        f.write(content)

def create_user_configuration_cs(app_name, app_dir):
    content = f'''using {app_name}.Core.Model.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace {app_name}.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{{
    public void Configure(EntityTypeBuilder<User> builder)
    {{
        builder.HasKey(u => u.UserId);

        builder.Property(u => u.UserId)
            .ValueGeneratedNever();

        builder.Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(u => u.Salt)
            .IsRequired();

        builder.HasIndex(u => u.UserName)
            .IsUnique();

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.HasMany(u => u.UserRoles)
            .WithOne()
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(u => u.UserRoles)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }}
}}
'''
    dir_path = os.path.join(app_dir, "src", f"{app_name}.Infrastructure", "Data", "Configurations")
    os.makedirs(dir_path, exist_ok=True)
    with open(os.path.join(dir_path, "UserConfiguration.cs"), "w") as f:
        f.write(content)

def create_role_configuration_cs(app_name, app_dir):
    content = f'''using {app_name}.Core.Model.UserAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace {app_name}.Infrastructure.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{{
    public void Configure(EntityTypeBuilder<Role> builder)
    {{
        builder.HasKey(r => r.RoleId);

        builder.Property(r => r.RoleId)
            .ValueGeneratedNever();

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(r => r.Name)
            .IsUnique();
    }}
}}
'''
    dir_path = os.path.join(app_dir, "src", f"{app_name}.Infrastructure", "Data", "Configurations")
    os.makedirs(dir_path, exist_ok=True)
    with open(os.path.join(dir_path, "RoleConfiguration.cs"), "w") as f:
        f.write(content)

def create_userrole_configuration_cs(app_name, app_dir):
    content = f'''using {app_name}.Core.Model.UserAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace {app_name}.Infrastructure.Data.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {{
        builder.HasKey(ur => new {{ ur.UserId, ur.RoleId }});

        builder.HasIndex(ur => ur.UserId);
        builder.HasIndex(ur => ur.RoleId);
    }}
}}
'''
    dir_path = os.path.join(app_dir, "src", f"{app_name}.Infrastructure", "Data", "Configurations")
    os.makedirs(dir_path, exist_ok=True)
    with open(os.path.join(dir_path, "UserRoleConfiguration.cs"), "w") as f:
        f.write(content)

def process_app(app_name):
    app_dir = os.path.join(APPS_DIR, app_name)

    # Create Core files
    create_user_cs(app_name, app_dir)
    create_role_cs(app_name, app_dir)
    create_userrole_cs(app_name, app_dir)
    create_ipassword_hasher_cs(app_name, app_dir)
    create_password_hasher_cs(app_name, app_dir)
    create_ijwt_token_service_cs(app_name, app_dir)
    create_jwt_token_service_cs(app_name, app_dir)
    create_constants_cs(app_name, app_dir)

    # Create Infrastructure configuration files
    create_user_configuration_cs(app_name, app_dir)
    create_role_configuration_cs(app_name, app_dir)
    create_userrole_configuration_cs(app_name, app_dir)

if __name__ == "__main__":
    apps = get_apps()
    print(f"Found {len(apps)} apps to process")

    for app in apps:
        print(f"Processing {app}...")
        try:
            process_app(app)
        except Exception as e:
            print(f"Error processing {app}: {e}")

    print("Done creating core and infrastructure files!")
