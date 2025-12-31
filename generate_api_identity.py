#!/usr/bin/env python3
"""Script to generate identity API implementation for all apps."""

import os

APPS_DIR = "/home/user/Apps"
SKIP_DIRS = ["FamilyCalendarEventPlanner", "AnniversaryBirthdayReminder", ".git", ".claude"]

def get_apps():
    apps = []
    for item in os.listdir(APPS_DIR):
        if os.path.isdir(os.path.join(APPS_DIR, item)) and item not in SKIP_DIRS:
            if os.path.isdir(os.path.join(APPS_DIR, item, "src")):
                apps.append(item)
    return sorted(apps)

def create_auth_controller(app_name, app_dir):
    content = f'''using {app_name}.Api.Features.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace {app_name}.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{{
    private readonly IMediator _mediator;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IMediator mediator, ILogger<AuthController> logger)
    {{
        _mediator = mediator;
        _logger = logger;
    }}

    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(LoginResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<LoginResult>> Login([FromBody] LoginCommand command)
    {{
        _logger.LogInformation("Login attempt for username: {{Username}}", command.Username);

        var result = await _mediator.Send(command);

        if (result == null)
        {{
            return Unauthorized(new {{ error = "Invalid username or password" }});
        }}

        return Ok(result);
    }}
}}
'''
    dir_path = os.path.join(app_dir, "src", f"{app_name}.Api", "Controllers")
    os.makedirs(dir_path, exist_ok=True)
    with open(os.path.join(dir_path, "AuthController.cs"), "w") as f:
        f.write(content)

def create_users_controller(app_name, app_dir):
    content = f'''using {app_name}.Api.Features.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace {app_name}.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class UsersController : ControllerBase
{{
    private readonly IMediator _mediator;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IMediator mediator, ILogger<UsersController> logger)
    {{
        _mediator = mediator;
        _logger = logger;
    }}

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {{
        _logger.LogInformation("Getting all users");
        var result = await _mediator.Send(new GetUsersQuery());
        return Ok(result);
    }}

    [HttpGet("{{userId:guid}}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> GetUserById(Guid userId)
    {{
        _logger.LogInformation("Getting user {{UserId}}", userId);
        var result = await _mediator.Send(new GetUserByIdQuery {{ UserId = userId }});
        if (result == null) return NotFound();
        return Ok(result);
    }}

    [HttpPost]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserCommand command)
    {{
        _logger.LogInformation("Creating user with username: {{UserName}}", command.UserName);
        try
        {{
            var result = await _mediator.Send(command);
            return Created($"/api/users/{{result.UserId}}", result);
        }}
        catch (InvalidOperationException ex)
        {{
            return BadRequest(new {{ error = ex.Message }});
        }}
    }}

    [HttpPut("{{userId:guid}}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDto>> UpdateUser(Guid userId, [FromBody] UpdateUserCommand command)
    {{
        if (userId != command.UserId) return BadRequest("User ID mismatch");
        _logger.LogInformation("Updating user {{UserId}}", userId);
        try
        {{
            var result = await _mediator.Send(command);
            if (result == null) return NotFound();
            return Ok(result);
        }}
        catch (InvalidOperationException ex)
        {{
            return BadRequest(new {{ error = ex.Message }});
        }}
    }}

    [HttpDelete("{{userId:guid}}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {{
        _logger.LogInformation("Deleting user {{UserId}}", userId);
        var result = await _mediator.Send(new DeleteUserCommand {{ UserId = userId }});
        if (!result) return NotFound();
        return NoContent();
    }}

    [HttpPost("{{userId:guid}}/roles")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDto>> AddRoleToUser(Guid userId, [FromBody] AddRoleRequest request)
    {{
        _logger.LogInformation("Adding role {{RoleId}} to user {{UserId}}", request.RoleId, userId);
        try
        {{
            var result = await _mediator.Send(new AddRoleToUserCommand {{ UserId = userId, RoleId = request.RoleId }});
            if (result == null) return NotFound();
            return Ok(result);
        }}
        catch (InvalidOperationException ex)
        {{
            return BadRequest(new {{ error = ex.Message }});
        }}
    }}

    [HttpDelete("{{userId:guid}}/roles/{{roleId:guid}}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> RemoveRoleFromUser(Guid userId, Guid roleId)
    {{
        _logger.LogInformation("Removing role {{RoleId}} from user {{UserId}}", roleId, userId);
        var result = await _mediator.Send(new RemoveRoleFromUserCommand {{ UserId = userId, RoleId = roleId }});
        if (result == null) return NotFound();
        return Ok(result);
    }}
}}

public record AddRoleRequest
{{
    public Guid RoleId {{ get; init; }}
}}
'''
    dir_path = os.path.join(app_dir, "src", f"{app_name}.Api", "Controllers")
    os.makedirs(dir_path, exist_ok=True)
    with open(os.path.join(dir_path, "UsersController.cs"), "w") as f:
        f.write(content)

def create_roles_controller(app_name, app_dir):
    content = f'''using {app_name}.Api.Features.Roles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace {app_name}.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class RolesController : ControllerBase
{{
    private readonly IMediator _mediator;
    private readonly ILogger<RolesController> _logger;

    public RolesController(IMediator mediator, ILogger<RolesController> logger)
    {{
        _mediator = mediator;
        _logger = logger;
    }}

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RoleDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<RoleDto>>> GetRoles()
    {{
        _logger.LogInformation("Getting all roles");
        var result = await _mediator.Send(new GetRolesQuery());
        return Ok(result);
    }}

    [HttpGet("{{roleId:guid}}")]
    [ProducesResponseType(typeof(RoleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RoleDto>> GetRoleById(Guid roleId)
    {{
        _logger.LogInformation("Getting role {{RoleId}}", roleId);
        var result = await _mediator.Send(new GetRoleByIdQuery {{ RoleId = roleId }});
        if (result == null) return NotFound();
        return Ok(result);
    }}

    [HttpPost]
    [ProducesResponseType(typeof(RoleDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RoleDto>> CreateRole([FromBody] CreateRoleCommand command)
    {{
        _logger.LogInformation("Creating role with name: {{Name}}", command.Name);
        try
        {{
            var result = await _mediator.Send(command);
            return Created($"/api/roles/{{result.RoleId}}", result);
        }}
        catch (InvalidOperationException ex)
        {{
            return BadRequest(new {{ error = ex.Message }});
        }}
    }}

    [HttpPut("{{roleId:guid}}")]
    [ProducesResponseType(typeof(RoleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RoleDto>> UpdateRole(Guid roleId, [FromBody] UpdateRoleCommand command)
    {{
        if (roleId != command.RoleId) return BadRequest("Role ID mismatch");
        _logger.LogInformation("Updating role {{RoleId}}", roleId);
        try
        {{
            var result = await _mediator.Send(command);
            if (result == null) return NotFound();
            return Ok(result);
        }}
        catch (InvalidOperationException ex)
        {{
            return BadRequest(new {{ error = ex.Message }});
        }}
    }}

    [HttpDelete("{{roleId:guid}}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRole(Guid roleId)
    {{
        _logger.LogInformation("Deleting role {{RoleId}}", roleId);
        var result = await _mediator.Send(new DeleteRoleCommand {{ RoleId = roleId }});
        if (!result) return NotFound();
        return NoContent();
    }}
}}
'''
    dir_path = os.path.join(app_dir, "src", f"{app_name}.Api", "Controllers")
    os.makedirs(dir_path, exist_ok=True)
    with open(os.path.join(dir_path, "RolesController.cs"), "w") as f:
        f.write(content)

def create_login_command(app_name, app_dir):
    content = f'''using {app_name}.Core;
using {app_name}.Core.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace {app_name}.Api.Features.Auth;

public record LoginCommand : IRequest<LoginResult?>
{{
    public string Username {{ get; init; }} = string.Empty;
    public string Password {{ get; init; }} = string.Empty;
}}

public record LoginResult
{{
    public string Token {{ get; init; }} = string.Empty;
    public DateTime ExpiresAt {{ get; init; }}
    public UserInfo User {{ get; init; }} = null!;
}}

public record UserInfo
{{
    public Guid UserId {{ get; init; }}
    public string UserName {{ get; init; }} = string.Empty;
    public string Email {{ get; init; }} = string.Empty;
    public List<string> Roles {{ get; init; }} = new();
}}

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResult?>
{{
    private readonly I{app_name}Context _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly ILogger<LoginCommandHandler> _logger;

    public LoginCommandHandler(
        I{app_name}Context context,
        IPasswordHasher passwordHasher,
        IJwtTokenService jwtTokenService,
        ILogger<LoginCommandHandler> logger)
    {{
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
        _logger = logger;
    }}

    public async Task<LoginResult?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {{
        _logger.LogInformation("Login attempt for username: {{Username}}", request.Username);

        var user = await _context.Users
            .Include(u => u.UserRoles)
            .FirstOrDefaultAsync(u => u.UserName == request.Username, cancellationToken);

        if (user == null)
        {{
            _logger.LogWarning("Login failed: User not found for username: {{Username}}", request.Username);
            return null;
        }}

        if (!_passwordHasher.VerifyPassword(request.Password, user.Password, user.Salt))
        {{
            _logger.LogWarning("Login failed: Invalid password for username: {{Username}}", request.Username);
            return null;
        }}

        var allRoles = await _context.Roles.AsNoTracking().ToListAsync(cancellationToken);
        var userRoleIds = user.UserRoles.Select(ur => ur.RoleId).ToHashSet();
        var userRoles = allRoles.Where(r => userRoleIds.Contains(r.RoleId)).ToList();

        var token = _jwtTokenService.GenerateToken(user, userRoles);
        var expiresAt = _jwtTokenService.GetTokenExpiration();

        _logger.LogInformation("Login successful for username: {{Username}}", request.Username);

        return new LoginResult
        {{
            Token = token,
            ExpiresAt = expiresAt,
            User = new UserInfo
            {{
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                Roles = userRoles.Select(r => r.Name).ToList()
            }}
        }};
    }}
}}
'''
    dir_path = os.path.join(app_dir, "src", f"{app_name}.Api", "Features", "Auth")
    os.makedirs(dir_path, exist_ok=True)
    with open(os.path.join(dir_path, "LoginCommand.cs"), "w") as f:
        f.write(content)

def create_role_dto(app_name, app_dir):
    content = f'''using {app_name}.Core.Model.UserAggregate.Entities;

namespace {app_name}.Api.Features.Roles;

public record RoleDto
{{
    public Guid RoleId {{ get; init; }}
    public string Name {{ get; init; }} = string.Empty;
}}

public static class RoleExtensions
{{
    public static RoleDto ToDto(this Role role)
    {{
        return new RoleDto
        {{
            RoleId = role.RoleId,
            Name = role.Name
        }};
    }}
}}
'''
    dir_path = os.path.join(app_dir, "src", f"{app_name}.Api", "Features", "Roles")
    os.makedirs(dir_path, exist_ok=True)
    with open(os.path.join(dir_path, "RoleDto.cs"), "w") as f:
        f.write(content)

def create_user_dto(app_name, app_dir):
    content = f'''using {app_name}.Core.Model.UserAggregate;
using {app_name}.Api.Features.Roles;

namespace {app_name}.Api.Features.Users;

public record UserDto
{{
    public Guid UserId {{ get; init; }}
    public string UserName {{ get; init; }} = string.Empty;
    public string Email {{ get; init; }} = string.Empty;
    public List<RoleDto> Roles {{ get; init; }} = new();
}}

public static class UserExtensions
{{
    public static UserDto ToDto(this User user, IEnumerable<Core.Model.UserAggregate.Entities.Role>? roles = null)
    {{
        var userRoleIds = user.UserRoles.Select(ur => ur.RoleId).ToHashSet();
        var userRoles = roles?
            .Where(r => userRoleIds.Contains(r.RoleId))
            .Select(r => r.ToDto())
            .ToList() ?? new List<RoleDto>();

        return new UserDto
        {{
            UserId = user.UserId,
            UserName = user.UserName,
            Email = user.Email,
            Roles = userRoles
        }};
    }}
}}
'''
    dir_path = os.path.join(app_dir, "src", f"{app_name}.Api", "Features", "Users")
    os.makedirs(dir_path, exist_ok=True)
    with open(os.path.join(dir_path, "UserDto.cs"), "w") as f:
        f.write(content)

def create_get_users_query(app_name, app_dir):
    content = f'''using {app_name}.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace {app_name}.Api.Features.Users;

public record GetUsersQuery : IRequest<IEnumerable<UserDto>> {{ }}

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
{{
    private readonly I{app_name}Context _context;
    private readonly ILogger<GetUsersQueryHandler> _logger;

    public GetUsersQueryHandler(I{app_name}Context context, ILogger<GetUsersQueryHandler> logger)
    {{
        _context = context;
        _logger = logger;
    }}

    public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {{
        _logger.LogInformation("Getting all users");
        var users = await _context.Users.Include(u => u.UserRoles).AsNoTracking().OrderBy(u => u.UserName).ToListAsync(cancellationToken);
        var roles = await _context.Roles.AsNoTracking().ToListAsync(cancellationToken);
        return users.Select(u => u.ToDto(roles));
    }}
}}
'''
    dir_path = os.path.join(app_dir, "src", f"{app_name}.Api", "Features", "Users")
    with open(os.path.join(dir_path, "GetUsersQuery.cs"), "w") as f:
        f.write(content)

def create_get_user_by_id_query(app_name, app_dir):
    content = f'''using {app_name}.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace {app_name}.Api.Features.Users;

public record GetUserByIdQuery : IRequest<UserDto?>
{{
    public Guid UserId {{ get; init; }}
}}

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
{{
    private readonly I{app_name}Context _context;
    private readonly ILogger<GetUserByIdQueryHandler> _logger;

    public GetUserByIdQueryHandler(I{app_name}Context context, ILogger<GetUserByIdQueryHandler> logger)
    {{
        _context = context;
        _logger = logger;
    }}

    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {{
        _logger.LogInformation("Getting user {{UserId}}", request.UserId);
        var user = await _context.Users.Include(u => u.UserRoles).AsNoTracking().FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);
        if (user == null) return null;
        var roles = await _context.Roles.AsNoTracking().ToListAsync(cancellationToken);
        return user.ToDto(roles);
    }}
}}
'''
    dir_path = os.path.join(app_dir, "src", f"{app_name}.Api", "Features", "Users")
    with open(os.path.join(dir_path, "GetUserByIdQuery.cs"), "w") as f:
        f.write(content)

def create_create_user_command(app_name, app_dir):
    content = f'''using {app_name}.Core;
using {app_name}.Core.Model.UserAggregate;
using {app_name}.Core.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace {app_name}.Api.Features.Users;

public record CreateUserCommand : IRequest<UserDto>
{{
    public string UserName {{ get; init; }} = string.Empty;
    public string Email {{ get; init; }} = string.Empty;
    public string Password {{ get; init; }} = string.Empty;
    public List<Guid>? RoleIds {{ get; init; }}
}}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{{
    private readonly I{app_name}Context _context;
    private readonly ITenantContext _tenantContext;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<CreateUserCommandHandler> _logger;

    public CreateUserCommandHandler(I{app_name}Context context, ITenantContext tenantContext, IPasswordHasher passwordHasher, ILogger<CreateUserCommandHandler> logger)
    {{
        _context = context;
        _tenantContext = tenantContext;
        _passwordHasher = passwordHasher;
        _logger = logger;
    }}

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {{
        _logger.LogInformation("Creating user with username: {{UserName}}", request.UserName);
        if (await _context.Users.AnyAsync(u => u.UserName == request.UserName, cancellationToken))
            throw new InvalidOperationException($"Username '{{request.UserName}}' is already taken.");
        if (await _context.Users.AnyAsync(u => u.Email == request.Email, cancellationToken))
            throw new InvalidOperationException($"Email '{{request.Email}}' is already registered.");

        var (hashedPassword, salt) = _passwordHasher.HashPassword(request.Password);
        var tenantId = _tenantContext.TenantId != Guid.Empty ? _tenantContext.TenantId : Constants.DefaultTenantId;
        var user = new User(tenantId, request.UserName, request.Email, hashedPassword, salt);

        if (request.RoleIds != null && request.RoleIds.Any())
        {{
            var roles = await _context.Roles.Where(r => request.RoleIds.Contains(r.RoleId)).ToListAsync(cancellationToken);
            foreach (var role in roles) user.AddRole(role);
        }}

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Created user {{UserId}}", user.UserId);
        var allRoles = await _context.Roles.AsNoTracking().ToListAsync(cancellationToken);
        return user.ToDto(allRoles);
    }}
}}
'''
    dir_path = os.path.join(app_dir, "src", f"{app_name}.Api", "Features", "Users")
    with open(os.path.join(dir_path, "CreateUserCommand.cs"), "w") as f:
        f.write(content)

def create_update_user_command(app_name, app_dir):
    content = f'''using {app_name}.Core;
using {app_name}.Core.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace {app_name}.Api.Features.Users;

public record UpdateUserCommand : IRequest<UserDto?>
{{
    public Guid UserId {{ get; init; }}
    public string? UserName {{ get; init; }}
    public string? Email {{ get; init; }}
    public string? Password {{ get; init; }}
}}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto?>
{{
    private readonly I{app_name}Context _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<UpdateUserCommandHandler> _logger;

    public UpdateUserCommandHandler(I{app_name}Context context, IPasswordHasher passwordHasher, ILogger<UpdateUserCommandHandler> logger)
    {{
        _context = context;
        _passwordHasher = passwordHasher;
        _logger = logger;
    }}

    public async Task<UserDto?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {{
        _logger.LogInformation("Updating user {{UserId}}", request.UserId);
        var user = await _context.Users.Include(u => u.UserRoles).FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);
        if (user == null) return null;

        if (!string.IsNullOrEmpty(request.UserName) && request.UserName != user.UserName)
            if (await _context.Users.AnyAsync(u => u.UserName == request.UserName && u.UserId != request.UserId, cancellationToken))
                throw new InvalidOperationException($"Username '{{request.UserName}}' is already taken.");

        if (!string.IsNullOrEmpty(request.Email) && request.Email != user.Email)
            if (await _context.Users.AnyAsync(u => u.Email == request.Email && u.UserId != request.UserId, cancellationToken))
                throw new InvalidOperationException($"Email '{{request.Email}}' is already registered.");

        user.UpdateProfile(request.UserName, request.Email);
        if (!string.IsNullOrEmpty(request.Password))
        {{
            var (hashedPassword, salt) = _passwordHasher.HashPassword(request.Password);
            user.SetPassword(hashedPassword, salt);
        }}
        await _context.SaveChangesAsync(cancellationToken);
        var roles = await _context.Roles.AsNoTracking().ToListAsync(cancellationToken);
        return user.ToDto(roles);
    }}
}}
'''
    dir_path = os.path.join(app_dir, "src", f"{app_name}.Api", "Features", "Users")
    with open(os.path.join(dir_path, "UpdateUserCommand.cs"), "w") as f:
        f.write(content)

def create_delete_user_command(app_name, app_dir):
    content = f'''using {app_name}.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace {app_name}.Api.Features.Users;

public record DeleteUserCommand : IRequest<bool>
{{
    public Guid UserId {{ get; init; }}
}}

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{{
    private readonly I{app_name}Context _context;
    private readonly ILogger<DeleteUserCommandHandler> _logger;

    public DeleteUserCommandHandler(I{app_name}Context context, ILogger<DeleteUserCommandHandler> logger)
    {{
        _context = context;
        _logger = logger;
    }}

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {{
        _logger.LogInformation("Deleting user {{UserId}}", request.UserId);
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);
        if (user == null) return false;
        _context.Users.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }}
}}
'''
    dir_path = os.path.join(app_dir, "src", f"{app_name}.Api", "Features", "Users")
    with open(os.path.join(dir_path, "DeleteUserCommand.cs"), "w") as f:
        f.write(content)

def create_add_role_to_user_command(app_name, app_dir):
    content = f'''using {app_name}.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace {app_name}.Api.Features.Users;

public record AddRoleToUserCommand : IRequest<UserDto?>
{{
    public Guid UserId {{ get; init; }}
    public Guid RoleId {{ get; init; }}
}}

public class AddRoleToUserCommandHandler : IRequestHandler<AddRoleToUserCommand, UserDto?>
{{
    private readonly I{app_name}Context _context;
    private readonly ILogger<AddRoleToUserCommandHandler> _logger;

    public AddRoleToUserCommandHandler(I{app_name}Context context, ILogger<AddRoleToUserCommandHandler> logger)
    {{
        _context = context;
        _logger = logger;
    }}

    public async Task<UserDto?> Handle(AddRoleToUserCommand request, CancellationToken cancellationToken)
    {{
        _logger.LogInformation("Adding role {{RoleId}} to user {{UserId}}", request.RoleId, request.UserId);
        var user = await _context.Users.Include(u => u.UserRoles).FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);
        if (user == null) return null;
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == request.RoleId, cancellationToken);
        if (role == null) throw new InvalidOperationException($"Role with ID '{{request.RoleId}}' not found.");
        user.AddRole(role);
        await _context.SaveChangesAsync(cancellationToken);
        var roles = await _context.Roles.AsNoTracking().ToListAsync(cancellationToken);
        return user.ToDto(roles);
    }}
}}
'''
    dir_path = os.path.join(app_dir, "src", f"{app_name}.Api", "Features", "Users")
    with open(os.path.join(dir_path, "AddRoleToUserCommand.cs"), "w") as f:
        f.write(content)

def create_remove_role_from_user_command(app_name, app_dir):
    content = f'''using {app_name}.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace {app_name}.Api.Features.Users;

public record RemoveRoleFromUserCommand : IRequest<UserDto?>
{{
    public Guid UserId {{ get; init; }}
    public Guid RoleId {{ get; init; }}
}}

public class RemoveRoleFromUserCommandHandler : IRequestHandler<RemoveRoleFromUserCommand, UserDto?>
{{
    private readonly I{app_name}Context _context;
    private readonly ILogger<RemoveRoleFromUserCommandHandler> _logger;

    public RemoveRoleFromUserCommandHandler(I{app_name}Context context, ILogger<RemoveRoleFromUserCommandHandler> logger)
    {{
        _context = context;
        _logger = logger;
    }}

    public async Task<UserDto?> Handle(RemoveRoleFromUserCommand request, CancellationToken cancellationToken)
    {{
        _logger.LogInformation("Removing role {{RoleId}} from user {{UserId}}", request.RoleId, request.UserId);
        var user = await _context.Users.Include(u => u.UserRoles).FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);
        if (user == null) return null;
        user.RemoveRole(request.RoleId);
        await _context.SaveChangesAsync(cancellationToken);
        var roles = await _context.Roles.AsNoTracking().ToListAsync(cancellationToken);
        return user.ToDto(roles);
    }}
}}
'''
    dir_path = os.path.join(app_dir, "src", f"{app_name}.Api", "Features", "Users")
    with open(os.path.join(dir_path, "RemoveRoleFromUserCommand.cs"), "w") as f:
        f.write(content)

def create_get_roles_query(app_name, app_dir):
    content = f'''using {app_name}.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace {app_name}.Api.Features.Roles;

public record GetRolesQuery : IRequest<IEnumerable<RoleDto>> {{ }}

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, IEnumerable<RoleDto>>
{{
    private readonly I{app_name}Context _context;
    private readonly ILogger<GetRolesQueryHandler> _logger;

    public GetRolesQueryHandler(I{app_name}Context context, ILogger<GetRolesQueryHandler> logger)
    {{
        _context = context;
        _logger = logger;
    }}

    public async Task<IEnumerable<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {{
        _logger.LogInformation("Getting all roles");
        var roles = await _context.Roles.AsNoTracking().OrderBy(r => r.Name).ToListAsync(cancellationToken);
        return roles.Select(r => r.ToDto());
    }}
}}
'''
    dir_path = os.path.join(app_dir, "src", f"{app_name}.Api", "Features", "Roles")
    with open(os.path.join(dir_path, "GetRolesQuery.cs"), "w") as f:
        f.write(content)

def create_get_role_by_id_query(app_name, app_dir):
    content = f'''using {app_name}.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace {app_name}.Api.Features.Roles;

public record GetRoleByIdQuery : IRequest<RoleDto?>
{{
    public Guid RoleId {{ get; init; }}
}}

public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleDto?>
{{
    private readonly I{app_name}Context _context;
    private readonly ILogger<GetRoleByIdQueryHandler> _logger;

    public GetRoleByIdQueryHandler(I{app_name}Context context, ILogger<GetRoleByIdQueryHandler> logger)
    {{
        _context = context;
        _logger = logger;
    }}

    public async Task<RoleDto?> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {{
        _logger.LogInformation("Getting role {{RoleId}}", request.RoleId);
        var role = await _context.Roles.AsNoTracking().FirstOrDefaultAsync(r => r.RoleId == request.RoleId, cancellationToken);
        return role?.ToDto();
    }}
}}
'''
    dir_path = os.path.join(app_dir, "src", f"{app_name}.Api", "Features", "Roles")
    with open(os.path.join(dir_path, "GetRoleByIdQuery.cs"), "w") as f:
        f.write(content)

def create_create_role_command(app_name, app_dir):
    content = f'''using {app_name}.Core;
using {app_name}.Core.Model.UserAggregate.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace {app_name}.Api.Features.Roles;

public record CreateRoleCommand : IRequest<RoleDto>
{{
    public string Name {{ get; init; }} = string.Empty;
}}

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, RoleDto>
{{
    private readonly I{app_name}Context _context;
    private readonly ITenantContext _tenantContext;
    private readonly ILogger<CreateRoleCommandHandler> _logger;

    public CreateRoleCommandHandler(I{app_name}Context context, ITenantContext tenantContext, ILogger<CreateRoleCommandHandler> logger)
    {{
        _context = context;
        _tenantContext = tenantContext;
        _logger = logger;
    }}

    public async Task<RoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {{
        _logger.LogInformation("Creating role with name: {{Name}}", request.Name);
        if (await _context.Roles.AnyAsync(r => r.Name == request.Name, cancellationToken))
            throw new InvalidOperationException($"Role with name '{{request.Name}}' already exists.");

        var tenantId = _tenantContext.TenantId != Guid.Empty ? _tenantContext.TenantId : Constants.DefaultTenantId;
        var role = new Role(tenantId, request.Name);
        _context.Roles.Add(role);
        await _context.SaveChangesAsync(cancellationToken);
        return role.ToDto();
    }}
}}
'''
    dir_path = os.path.join(app_dir, "src", f"{app_name}.Api", "Features", "Roles")
    with open(os.path.join(dir_path, "CreateRoleCommand.cs"), "w") as f:
        f.write(content)

def create_update_role_command(app_name, app_dir):
    content = f'''using {app_name}.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace {app_name}.Api.Features.Roles;

public record UpdateRoleCommand : IRequest<RoleDto?>
{{
    public Guid RoleId {{ get; init; }}
    public string Name {{ get; init; }} = string.Empty;
}}

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, RoleDto?>
{{
    private readonly I{app_name}Context _context;
    private readonly ILogger<UpdateRoleCommandHandler> _logger;

    public UpdateRoleCommandHandler(I{app_name}Context context, ILogger<UpdateRoleCommandHandler> logger)
    {{
        _context = context;
        _logger = logger;
    }}

    public async Task<RoleDto?> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {{
        _logger.LogInformation("Updating role {{RoleId}}", request.RoleId);
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == request.RoleId, cancellationToken);
        if (role == null) return null;
        if (request.Name != role.Name && await _context.Roles.AnyAsync(r => r.Name == request.Name && r.RoleId != request.RoleId, cancellationToken))
            throw new InvalidOperationException($"Role with name '{{request.Name}}' already exists.");
        role.UpdateName(request.Name);
        await _context.SaveChangesAsync(cancellationToken);
        return role.ToDto();
    }}
}}
'''
    dir_path = os.path.join(app_dir, "src", f"{app_name}.Api", "Features", "Roles")
    with open(os.path.join(dir_path, "UpdateRoleCommand.cs"), "w") as f:
        f.write(content)

def create_delete_role_command(app_name, app_dir):
    content = f'''using {app_name}.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace {app_name}.Api.Features.Roles;

public record DeleteRoleCommand : IRequest<bool>
{{
    public Guid RoleId {{ get; init; }}
}}

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, bool>
{{
    private readonly I{app_name}Context _context;
    private readonly ILogger<DeleteRoleCommandHandler> _logger;

    public DeleteRoleCommandHandler(I{app_name}Context context, ILogger<DeleteRoleCommandHandler> logger)
    {{
        _context = context;
        _logger = logger;
    }}

    public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {{
        _logger.LogInformation("Deleting role {{RoleId}}", request.RoleId);
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == request.RoleId, cancellationToken);
        if (role == null) return false;
        var userRoles = await _context.UserRoles.Where(ur => ur.RoleId == request.RoleId).ToListAsync(cancellationToken);
        foreach (var userRole in userRoles) _context.UserRoles.Remove(userRole);
        _context.Roles.Remove(role);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }}
}}
'''
    dir_path = os.path.join(app_dir, "src", f"{app_name}.Api", "Features", "Roles")
    with open(os.path.join(dir_path, "DeleteRoleCommand.cs"), "w") as f:
        f.write(content)

def process_app(app_name):
    app_dir = os.path.join(APPS_DIR, app_name)

    # Controllers
    create_auth_controller(app_name, app_dir)
    create_users_controller(app_name, app_dir)
    create_roles_controller(app_name, app_dir)

    # Features - Auth
    create_login_command(app_name, app_dir)

    # Features - Users
    create_user_dto(app_name, app_dir)
    create_get_users_query(app_name, app_dir)
    create_get_user_by_id_query(app_name, app_dir)
    create_create_user_command(app_name, app_dir)
    create_update_user_command(app_name, app_dir)
    create_delete_user_command(app_name, app_dir)
    create_add_role_to_user_command(app_name, app_dir)
    create_remove_role_from_user_command(app_name, app_dir)

    # Features - Roles
    create_role_dto(app_name, app_dir)
    create_get_roles_query(app_name, app_dir)
    create_get_role_by_id_query(app_name, app_dir)
    create_create_role_command(app_name, app_dir)
    create_update_role_command(app_name, app_dir)
    create_delete_role_command(app_name, app_dir)

if __name__ == "__main__":
    apps = get_apps()
    print(f"Found {len(apps)} apps to process")
    for app in apps:
        print(f"Processing {app}...")
        try:
            process_app(app)
        except Exception as e:
            print(f"Error processing {app}: {e}")
    print("Done creating API files!")
