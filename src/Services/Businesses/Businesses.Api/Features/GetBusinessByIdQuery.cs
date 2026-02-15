using Businesses.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Businesses.Api.Features;

public record GetBusinessByIdQuery(Guid BusinessId, Guid TenantId) : IRequest<BusinessDto?>;

public class GetBusinessByIdQueryHandler : IRequestHandler<GetBusinessByIdQuery, BusinessDto?>
{
    private readonly IBusinessesDbContext _context;

    public GetBusinessByIdQueryHandler(IBusinessesDbContext context)
    {
        _context = context;
    }

    public async Task<BusinessDto?> Handle(GetBusinessByIdQuery request, CancellationToken cancellationToken)
    {
        var business = await _context.Businesses
            .FirstOrDefaultAsync(b => b.BusinessId == request.BusinessId && b.TenantId == request.TenantId, cancellationToken);

        return business == null ? null : new BusinessDto(business.BusinessId, business.TenantId, business.UserId, business.Name, business.Description, business.Category, business.CreatedAt, business.UpdatedAt);
    }
}
