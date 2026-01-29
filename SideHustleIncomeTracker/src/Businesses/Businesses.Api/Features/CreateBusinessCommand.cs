using Businesses.Core;
using Businesses.Core.Models;
using MediatR;

namespace Businesses.Api.Features;

public record CreateBusinessCommand(Guid TenantId, Guid UserId, string Name, string? Description, string? Category) : IRequest<BusinessDto>;

public class CreateBusinessCommandHandler : IRequestHandler<CreateBusinessCommand, BusinessDto>
{
    private readonly IBusinessesDbContext _context;

    public CreateBusinessCommandHandler(IBusinessesDbContext context)
    {
        _context = context;
    }

    public async Task<BusinessDto> Handle(CreateBusinessCommand request, CancellationToken cancellationToken)
    {
        var business = new Business(request.TenantId, request.UserId, request.Name, request.Description, request.Category);

        _context.Businesses.Add(business);
        await _context.SaveChangesAsync(cancellationToken);

        return new BusinessDto(business.BusinessId, business.TenantId, business.UserId, business.Name, business.Description, business.Category, business.CreatedAt, business.UpdatedAt);
    }
}
