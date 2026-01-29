using DateManagement.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DateManagement.Api.Features;

public record GetImportantDateByIdQuery(Guid ImportantDateId) : IRequest<ImportantDateDto?>;

public class GetImportantDateByIdQueryHandler : IRequestHandler<GetImportantDateByIdQuery, ImportantDateDto?>
{
    private readonly IDateManagementDbContext _context;

    public GetImportantDateByIdQueryHandler(IDateManagementDbContext context)
    {
        _context = context;
    }

    public async Task<ImportantDateDto?> Handle(GetImportantDateByIdQuery request, CancellationToken cancellationToken)
    {
        var date = await _context.ImportantDates
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.ImportantDateId == request.ImportantDateId, cancellationToken);

        return date?.ToDto();
    }
}
