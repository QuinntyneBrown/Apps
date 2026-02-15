using Deductions.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Deductions.Api.Features;

public record GetDeductionsQuery : IRequest<IEnumerable<DeductionDto>>;

public class GetDeductionsQueryHandler : IRequestHandler<GetDeductionsQuery, IEnumerable<DeductionDto>>
{
    private readonly IDeductionsDbContext _context;

    public GetDeductionsQueryHandler(IDeductionsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DeductionDto>> Handle(GetDeductionsQuery request, CancellationToken cancellationToken)
    {
        var deductions = await _context.Deductions.ToListAsync(cancellationToken);
        return deductions.Select(d => d.ToDto());
    }
}
