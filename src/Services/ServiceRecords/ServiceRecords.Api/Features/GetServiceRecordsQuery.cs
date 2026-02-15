using ServiceRecords.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ServiceRecords.Api.Features;

public record GetServiceRecordsQuery : IRequest<IEnumerable<ServiceRecordDto>>;

public class GetServiceRecordsQueryHandler : IRequestHandler<GetServiceRecordsQuery, IEnumerable<ServiceRecordDto>>
{
    private readonly IServiceRecordsDbContext _context;

    public GetServiceRecordsQueryHandler(IServiceRecordsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ServiceRecordDto>> Handle(GetServiceRecordsQuery request, CancellationToken cancellationToken)
    {
        var records = await _context.ServiceRecords.ToListAsync(cancellationToken);
        return records.Select(r => r.ToDto());
    }
}
