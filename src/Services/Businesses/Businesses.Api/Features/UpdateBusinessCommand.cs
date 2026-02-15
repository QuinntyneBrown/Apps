using Businesses.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Businesses.Api.Features;

public record UpdateBusinessCommand(Guid BusinessId, Guid TenantId, string? Name, string? Description, string? Category) : IRequest<BusinessDto?>;

public class UpdateBusinessCommandHandler : IRequestHandler<UpdateBusinessCommand, BusinessDto?>
{
    private readonly IBusinessesDbContext _context;

    public UpdateBusinessCommandHandler(IBusinessesDbContext context)
    {
        _context = context;
    }

    public async Task<BusinessDto?> Handle(UpdateBusinessCommand request, CancellationToken cancellationToken)
    {
        var business = await _context.Businesses
            .FirstOrDefaultAsync(b => b.BusinessId == request.BusinessId && b.TenantId == request.TenantId, cancellationToken);

        if (business == null) return null;

        business.Update(request.Name, request.Description, request.Category);
        await _context.SaveChangesAsync(cancellationToken);

        return new BusinessDto(business.BusinessId, business.TenantId, business.UserId, business.Name, business.Description, business.Category, business.CreatedAt, business.UpdatedAt);
    }
}
