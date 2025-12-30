using FamilyCalendarEventPlanner.Core;
using FamilyCalendarEventPlanner.Core.Model.UserAggregate.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyCalendarEventPlanner.Api.Features.Roles;

public record CreateRoleCommand : IRequest<RoleDto>
{
    public string Name { get; init; } = string.Empty;
}

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, RoleDto>
{
    private readonly IFamilyCalendarEventPlannerContext _context;
    private readonly ILogger<CreateRoleCommandHandler> _logger;

    public CreateRoleCommandHandler(
        IFamilyCalendarEventPlannerContext context,
        ILogger<CreateRoleCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<RoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating role with name: {Name}", request.Name);

        // Check for existing role name
        var existingRole = await _context.Roles
            .AnyAsync(r => r.Name == request.Name, cancellationToken);
        if (existingRole)
        {
            throw new InvalidOperationException($"Role with name '{request.Name}' already exists.");
        }

        var role = new Role(request.Name);

        _context.Roles.Add(role);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Created role {RoleId} with name: {Name}", role.RoleId, request.Name);

        return role.ToDto();
    }
}
