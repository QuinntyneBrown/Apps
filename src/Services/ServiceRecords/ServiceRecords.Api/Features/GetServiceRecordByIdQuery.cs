using ServiceRecords.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ServiceRecords.Api.Features;

public record GetServiceRecordByIdQuery(Guid ServiceRecordId) : IRequest<ServiceRecordDto?>;

public class GetServiceRecordByIdQueryHandler : IRequestHandler<GetServiceRecordByIdQuery, ServiceRecordDto?>
{
    private readonly IServiceRecordsDbContext _context;

    public GetServiceRecordByIdQueryHandler(IServiceRecordsDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceRecordDto?> Handle(GetServiceRecordByIdQuery request, CancellationToken cancellationToken)
    {
        var record = await _context.ServiceRecords
            .FirstOrDefaultAsync(r => r.ServiceRecordId == request.ServiceRecordId, cancellationToken);
        return record?.ToDto();
    }
}
