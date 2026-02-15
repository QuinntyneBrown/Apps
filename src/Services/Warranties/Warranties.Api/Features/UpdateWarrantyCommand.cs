using Warranties.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Warranties.Api.Features;

public record UpdateWarrantyCommand(
    Guid WarrantyId,
    string? Provider,
    DateTime? StartDate,
    DateTime? EndDate,
    string? CoverageDetails,
    string? DocumentUrl) : IRequest<WarrantyDto?>;

public class UpdateWarrantyCommandHandler : IRequestHandler<UpdateWarrantyCommand, WarrantyDto?>
{
    private readonly IWarrantiesDbContext _context;

    public UpdateWarrantyCommandHandler(IWarrantiesDbContext context)
    {
        _context = context;
    }

    public async Task<WarrantyDto?> Handle(UpdateWarrantyCommand request, CancellationToken cancellationToken)
    {
        var warranty = await _context.Warranties
            .FirstOrDefaultAsync(w => w.WarrantyId == request.WarrantyId, cancellationToken);

        if (warranty == null) return null;

        warranty.Provider = request.Provider;
        warranty.StartDate = request.StartDate;
        warranty.EndDate = request.EndDate;
        warranty.CoverageDetails = request.CoverageDetails;
        warranty.DocumentUrl = request.DocumentUrl;

        await _context.SaveChangesAsync(cancellationToken);
        return warranty.ToDto();
    }
}
