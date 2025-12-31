#!/usr/bin/env python3
"""
Script to update remaining identity-related files for all apps:
- Context interfaces (add User, Role, UserRole DbSets)
- DbContext files (add DbSets and query filters)
- Program.cs (add JWT authentication)
- ConfigureServices.cs (add identity services)
- SeedData.cs (add admin user seeding)
"""

import os
import re

APPS_DIR = "/home/user/Apps"

# Skip directories that are not app directories
SKIP_DIRS = {".git", ".claude", "node_modules", "__pycache__", "FamilyCalendarEventPlanner", "AnniversaryBirthdayReminder"}

def get_app_dirs():
    """Get all app directories."""
    apps = []
    for name in os.listdir(APPS_DIR):
        path = os.path.join(APPS_DIR, name)
        if os.path.isdir(path) and name not in SKIP_DIRS and not name.endswith('.py'):
            # Check if it's an actual app directory (has src folder)
            src_path = os.path.join(path, "src")
            if os.path.isdir(src_path):
                apps.append(name)
    return sorted(apps)

def to_pascal_case(name):
    """Convert app name to PascalCase (already is for most)."""
    return name

def update_context_interface(app_name, app_dir):
    """Update the context interface to add User, Role, UserRole DbSets."""
    interface_path = os.path.join(app_dir, "src", f"{app_name}.Core", f"I{app_name}Context.cs")

    if not os.path.exists(interface_path):
        print(f"  Warning: Context interface not found: {interface_path}")
        return False

    with open(interface_path, 'r') as f:
        content = f.read()

    # Check if already has Users DbSet
    if "DbSet<User>" in content:
        return True  # Already updated

    # Add using statement for UserAggregate if not present
    if "using " + app_name + ".Core.Model.UserAggregate;" not in content:
        # Find the last using statement and add after it
        using_pattern = r'(using [^;]+;\s*\n)(?=\s*namespace)'
        match = re.search(using_pattern, content)
        if match:
            insert_pos = match.end()
            new_usings = f"using {app_name}.Core.Model.UserAggregate;\nusing {app_name}.Core.Model.UserAggregate.Entities;\n"
            content = content[:insert_pos] + new_usings + content[insert_pos:]

    # Find the interface body and add new DbSets before the closing brace
    # Look for the last DbSet declaration or SaveChangesAsync
    interface_pattern = r'(Task<int> SaveChangesAsync\(CancellationToken cancellationToken = default\);)'

    new_dbsets = """
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

    """

    if re.search(interface_pattern, content):
        content = re.sub(interface_pattern, new_dbsets + r'\1', content)

    with open(interface_path, 'w') as f:
        f.write(content)

    return True

def update_dbcontext(app_name, app_dir):
    """Update the DbContext to add User, Role, UserRole DbSets and query filters."""
    # DbContext is in Data folder
    context_path = os.path.join(app_dir, "src", f"{app_name}.Infrastructure", "Data", f"{app_name}Context.cs")

    if not os.path.exists(context_path):
        print(f"  Warning: DbContext not found: {context_path}")
        return False

    with open(context_path, 'r') as f:
        content = f.read()

    # Check if already has Users DbSet
    if "public DbSet<User> Users" in content:
        return True  # Already updated

    # Add using statements if not present
    if f"using {app_name}.Core.Model.UserAggregate;" not in content:
        using_pattern = r'(using [^;]+;\s*\n)(?=\s*namespace)'
        match = re.search(using_pattern, content)
        if match:
            insert_pos = match.end()
            new_usings = f"using {app_name}.Core.Model.UserAggregate;\nusing {app_name}.Core.Model.UserAggregate.Entities;\n"
            content = content[:insert_pos] + new_usings + content[insert_pos:]

    # Add DbSets - find the last DbSet property and add after it
    # Look for pattern like "public DbSet<...> ... { get; set; }"
    dbset_pattern = r'(public DbSet<\w+> \w+ \{ get; set; \} = null!;\s*\n)'
    matches = list(re.finditer(dbset_pattern, content))

    if matches:
        last_match = matches[-1]
        insert_pos = last_match.end()

        new_dbsets = f"""
    /// <summary>
    /// Gets or sets the users.
    /// </summary>
    public DbSet<User> Users {{ get; set; }} = null!;

    /// <summary>
    /// Gets or sets the roles.
    /// </summary>
    public DbSet<Role> Roles {{ get; set; }} = null!;

    /// <summary>
    /// Gets or sets the user roles.
    /// </summary>
    public DbSet<UserRole> UserRoles {{ get; set; }} = null!;
"""
        content = content[:insert_pos] + new_dbsets + content[insert_pos:]

    # Add query filters in OnModelCreating
    # Find the OnModelCreating method and add filters before the closing brace
    onmodelcreating_pattern = r'(protected override void OnModelCreating\(ModelBuilder modelBuilder\)\s*\{[^}]*)(base\.OnModelCreating\(modelBuilder\);)'

    query_filters = f"""
        // Apply tenant filter to User
        modelBuilder.Entity<User>().HasQueryFilter(u => u.TenantId == _tenantContext.TenantId);

        // Apply tenant filter to Role
        modelBuilder.Entity<Role>().HasQueryFilter(r => r.TenantId == _tenantContext.TenantId);

        """

    if re.search(onmodelcreating_pattern, content):
        content = re.sub(onmodelcreating_pattern, r'\1' + query_filters + r'\2', content)

    with open(context_path, 'w') as f:
        f.write(content)

    return True

def update_program_cs(app_name, app_dir):
    """Update Program.cs to add JWT authentication configuration."""
    program_path = os.path.join(app_dir, "src", f"{app_name}.Api", "Program.cs")

    if not os.path.exists(program_path):
        print(f"  Warning: Program.cs not found: {program_path}")
        return False

    with open(program_path, 'r') as f:
        content = f.read()

    # Check if already has JWT configuration
    if "AddAuthentication" in content or "JwtBearerDefaults" in content:
        return True  # Already updated

    # Add using statements
    new_usings = """using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
"""

    # Find the first using statement and add before it
    using_match = re.search(r'^using ', content, re.MULTILINE)
    if using_match:
        content = content[:using_match.start()] + new_usings + content[using_match.start():]

    # Add JWT configuration after AddInfrastructureServices
    jwt_config = """

// Configure JWT Authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"] ?? "YourSuperSecretKeyThatIsAtLeast32CharactersLong!");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"] ?? "YourApp",
        ValidAudience = jwtSettings["Audience"] ?? "YourApp",
        IssuerSigningKey = new SymmetricSecurityKey(key),
    };
});

builder.Services.AddAuthorization();
"""

    # Find AddInfrastructureServices and add after it
    infra_pattern = r'(builder\.Services\.AddInfrastructureServices\([^)]+\);)'
    if re.search(infra_pattern, content):
        content = re.sub(infra_pattern, r'\1' + jwt_config, content)

    # Add Swagger security definition
    swagger_security = """
    // Add JWT security definition
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer",
                },
            },
            Array.Empty<string>()
        },
    });
"""

    # Find AddSwaggerGen and add security config inside
    swagger_pattern = r'(builder\.Services\.AddSwaggerGen\(\s*options\s*=>\s*\{[^}]*)(options\.SwaggerDoc)'
    if re.search(swagger_pattern, content, re.DOTALL):
        content = re.sub(swagger_pattern, r'\1' + swagger_security + r'\n    \2', content, flags=re.DOTALL)

    # Add UseAuthentication and UseAuthorization before UseHttpsRedirection if not present
    if "app.UseAuthentication();" not in content:
        auth_middleware = """
app.UseAuthentication();
app.UseAuthorization();
"""
        https_pattern = r'(app\.UseHttpsRedirection\(\);)'
        if re.search(https_pattern, content):
            content = re.sub(https_pattern, auth_middleware + r'\n\1', content)

    with open(program_path, 'w') as f:
        f.write(content)

    return True

def update_configure_services(app_name, app_dir):
    """Update ConfigureServices.cs to add identity service registrations."""
    config_path = os.path.join(app_dir, "src", f"{app_name}.Infrastructure", "ConfigureServices.cs")

    if not os.path.exists(config_path):
        print(f"  Warning: ConfigureServices.cs not found: {config_path}")
        return False

    with open(config_path, 'r') as f:
        content = f.read()

    # Check if already has identity services
    if "IPasswordHasher" in content or "IJwtTokenService" in content:
        return True  # Already updated

    # Add using statement for Services
    if f"using {app_name}.Core.Services;" not in content:
        using_pattern = r'(using [^;]+;\s*\n)(?=\s*namespace)'
        match = re.search(using_pattern, content)
        if match:
            insert_pos = match.end()
            new_using = f"using {app_name}.Core.Services;\n"
            content = content[:insert_pos] + new_using + content[insert_pos:]

    # Add identity services before the return statement
    identity_services = """
        // Register identity services
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<IJwtTokenService, JwtTokenService>();

"""

    return_pattern = r'(\s*return services;)'
    if re.search(return_pattern, content):
        content = re.sub(return_pattern, identity_services + r'\1', content)

    with open(config_path, 'w') as f:
        f.write(content)

    return True

def update_seed_data(app_name, app_dir):
    """Update SeedData.cs to add role and admin user seeding."""
    seed_path = os.path.join(app_dir, "src", f"{app_name}.Infrastructure", "Data", "SeedData.cs")

    if not os.path.exists(seed_path):
        print(f"  Warning: SeedData.cs not found: {seed_path}")
        return False

    with open(seed_path, 'r') as f:
        content = f.read()

    # Check if already has role seeding
    if "SeedRolesAndAdminUserAsync" in content:
        return True  # Already updated

    # Add using statements
    if f"using {app_name}.Core.Model.UserAggregate;" not in content:
        using_pattern = r'(using [^;]+;\s*\n)(?=\s*namespace)'
        match = re.search(using_pattern, content)
        if match:
            insert_pos = match.end()
            new_usings = f"using {app_name}.Core.Model.UserAggregate;\nusing {app_name}.Core.Model.UserAggregate.Entities;\nusing {app_name}.Core.Services;\n"
            content = content[:insert_pos] + new_usings + content[insert_pos:]

    # Update SeedAsync method signature to include IPasswordHasher
    # Find the SeedAsync method and update its signature
    seed_async_pattern = r'public static async Task SeedAsync\((\w+Context context), ILogger logger\)'
    seed_async_replacement = r'public static async Task SeedAsync(\1 context, ILogger logger, IPasswordHasher passwordHasher)'
    content = re.sub(seed_async_pattern, seed_async_replacement, content)

    # Add passwordHasher null check
    if "ArgumentNullException.ThrowIfNull(passwordHasher);" not in content:
        logger_null_check = r'(ArgumentNullException\.ThrowIfNull\(logger\);)'
        content = re.sub(logger_null_check, r'\1\n        ArgumentNullException.ThrowIfNull(passwordHasher);', content)

    # Add call to SeedRolesAndAdminUserAsync after EnsureCreatedAsync
    if "SeedRolesAndAdminUserAsync" not in content:
        ensure_created_pattern = r'(await context\.Database\.EnsureCreatedAsync\(\);)'
        content = re.sub(ensure_created_pattern, r'\1\n\n            await SeedRolesAndAdminUserAsync(context, logger, passwordHasher);', content)

    # Add the SeedRolesAndAdminUserAsync method before the last closing brace
    seed_roles_method = f"""
    private static async Task SeedRolesAndAdminUserAsync(
        {app_name}Context context,
        ILogger logger,
        IPasswordHasher passwordHasher)
    {{
        var defaultTenantId = Constants.DefaultTenantId;

        var adminRoleName = "Admin";
        var userRoleName = "User";

        var adminRole = context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.Name == adminRoleName);
        if (adminRole == null)
        {{
            adminRole = new Role(defaultTenantId, adminRoleName);
            context.Roles.Add(adminRole);
            logger.LogInformation("Created Admin role.");
        }}

        var userRole = context.Roles.IgnoreQueryFilters().FirstOrDefault(r => r.Name == userRoleName);
        if (userRole == null)
        {{
            userRole = new Role(defaultTenantId, userRoleName);
            context.Roles.Add(userRole);
            logger.LogInformation("Created User role.");
        }}

        await context.SaveChangesAsync();

        var adminUserName = "admin";
        var adminUser = context.Users.IgnoreQueryFilters().FirstOrDefault(u => u.UserName == adminUserName);
        if (adminUser == null)
        {{
            var (hashedPassword, salt) = passwordHasher.HashPassword("Admin123!");
            adminUser = new User(
                tenantId: defaultTenantId,
                userName: adminUserName,
                email: "admin@{app_name.lower()}.local",
                hashedPassword: hashedPassword,
                salt: salt);

            adminUser.AddRole(adminRole);
            context.Users.Add(adminUser);
            await context.SaveChangesAsync();

            logger.LogInformation("Created admin user with Admin role.");
        }}
    }}
"""

    # Find the last closing brace of the class and add the method before it
    # Look for closing brace followed by newline and another closing brace (class end then namespace end)
    class_end_pattern = r'(\n}\s*\n})\s*$'
    if re.search(class_end_pattern, content):
        content = re.sub(class_end_pattern, seed_roles_method + r'\1', content)

    with open(seed_path, 'w') as f:
        f.write(content)

    return True

def main():
    apps = get_app_dirs()
    print(f"Found {len(apps)} apps to process")

    for app_name in apps:
        app_dir = os.path.join(APPS_DIR, app_name)
        print(f"Processing {app_name}...")

        # Update context interface
        update_context_interface(app_name, app_dir)

        # Update DbContext
        update_dbcontext(app_name, app_dir)

        # Update Program.cs
        update_program_cs(app_name, app_dir)

        # Update ConfigureServices.cs
        update_configure_services(app_name, app_dir)

        # Update SeedData.cs
        update_seed_data(app_name, app_dir)

    print("Done updating remaining files!")

if __name__ == "__main__":
    main()
