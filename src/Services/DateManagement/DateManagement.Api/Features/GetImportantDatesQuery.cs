using DateManagement.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DateManagement.Api.Features;

public record GetImportantDatesQuery : IRequest<IEnumerable<ImportantDateDto>>;

public class GetImportantDatesQueryHandler : IRequestHandler<GetImportantDatesQuery, IEnumerable<ImportantDateDto>>
{
    private readonly IDateManagementDbContext _context;

    public GetImportantDatesQueryHandler(IDateManagementDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ImportantDateDto>> Handle(GetImportantDatesQuery request, CancellationToken cancellationToken)
    {
        var dates = await _context.ImportantDates
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return dates.Select(d => d.ToDto());
    }
}
