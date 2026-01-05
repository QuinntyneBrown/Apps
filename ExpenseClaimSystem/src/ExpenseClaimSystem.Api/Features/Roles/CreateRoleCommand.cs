using ExpenseClaimSystem.Core;
using ExpenseClaimSystem.Core.Models.UserAggregate.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExpenseClaimSystem.Api.Features.Roles;

public record CreateRoleCommand : IRequest<RoleDto>
{
    public string Name { get; init; } = string.Empty;
}

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, RoleDto>
{
    private readonly IExpenseClaimSystemContext _context;
    private readonly ITenantContext _tenantContext;
    private readonly ILogger<CreateRoleCommandHandler> _logger;

    public CreateRoleCommandHandler(IExpenseClaimSystemContext context, ITenantContext tenantContext, ILogger<CreateRoleCommandHandler> logger)
    {
        _context = context;
        _tenantContext = tenantContext;
        _logger = logger;
    }

    public async Task<RoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating role with name: {Name}", request.Name);
        if (await _context.Roles.AnyAsync(r => r.Name == request.Name, cancellationToken))
            throw new InvalidOperationException($"Role with name '{request.Name}' already exists.");

        var tenantId = _tenantContext.TenantId != Guid.Empty ? _tenantContext.TenantId : Constants.DefaultTenantId;
        var role = new Role(tenantId, request.Name);
        _context.Roles.Add(role);
        await _context.SaveChangesAsync(cancellationToken);
        return role.ToDto();
    }
}
