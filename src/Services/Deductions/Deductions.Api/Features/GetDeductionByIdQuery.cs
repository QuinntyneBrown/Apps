using Deductions.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Deductions.Api.Features;

public record GetDeductionByIdQuery(Guid DeductionId) : IRequest<DeductionDto?>;

public class GetDeductionByIdQueryHandler : IRequestHandler<GetDeductionByIdQuery, DeductionDto?>
{
    private readonly IDeductionsDbContext _context;

    public GetDeductionByIdQueryHandler(IDeductionsDbContext context)
    {
        _context = context;
    }

    public async Task<DeductionDto?> Handle(GetDeductionByIdQuery request, CancellationToken cancellationToken)
    {
        var deduction = await _context.Deductions
            .FirstOrDefaultAsync(d => d.DeductionId == request.DeductionId, cancellationToken);
        return deduction?.ToDto();
    }
}
