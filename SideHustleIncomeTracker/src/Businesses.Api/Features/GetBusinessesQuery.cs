using Businesses.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Businesses.Api.Features;

public record GetBusinessesQuery(Guid TenantId, Guid UserId) : IRequest<IEnumerable<BusinessDto>>;

public class GetBusinessesQueryHandler : IRequestHandler<GetBusinessesQuery, IEnumerable<BusinessDto>>
{
    private readonly IBusinessesDbContext _context;

    public GetBusinessesQueryHandler(IBusinessesDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BusinessDto>> Handle(GetBusinessesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Businesses
            .Where(b => b.TenantId == request.TenantId && b.UserId == request.UserId)
            .Select(b => new BusinessDto(b.BusinessId, b.TenantId, b.UserId, b.Name, b.Description, b.Category, b.CreatedAt, b.UpdatedAt))
            .ToListAsync(cancellationToken);
    }
}
