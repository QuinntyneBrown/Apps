using ServiceRecords.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ServiceRecords.Api.Features;

public record UpdateServiceRecordCommand(
    Guid ServiceRecordId,
    DateTime ServiceDate,
    string? ServiceProvider,
    string? Description,
    decimal? Cost) : IRequest<ServiceRecordDto?>;

public class UpdateServiceRecordCommandHandler : IRequestHandler<UpdateServiceRecordCommand, ServiceRecordDto?>
{
    private readonly IServiceRecordsDbContext _context;

    public UpdateServiceRecordCommandHandler(IServiceRecordsDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceRecordDto?> Handle(UpdateServiceRecordCommand request, CancellationToken cancellationToken)
    {
        var record = await _context.ServiceRecords
            .FirstOrDefaultAsync(r => r.ServiceRecordId == request.ServiceRecordId, cancellationToken);

        if (record == null) return null;

        record.ServiceDate = request.ServiceDate;
        record.ServiceProvider = request.ServiceProvider;
        record.Description = request.Description;
        record.Cost = request.Cost;

        await _context.SaveChangesAsync(cancellationToken);
        return record.ToDto();
    }
}
