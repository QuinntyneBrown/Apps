using ServiceRecords.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ServiceRecords.Api.Features;

public record DeleteServiceRecordCommand(Guid ServiceRecordId) : IRequest<bool>;

public class DeleteServiceRecordCommandHandler : IRequestHandler<DeleteServiceRecordCommand, bool>
{
    private readonly IServiceRecordsDbContext _context;

    public DeleteServiceRecordCommandHandler(IServiceRecordsDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteServiceRecordCommand request, CancellationToken cancellationToken)
    {
        var record = await _context.ServiceRecords
            .FirstOrDefaultAsync(r => r.ServiceRecordId == request.ServiceRecordId, cancellationToken);

        if (record == null) return false;

        _context.ServiceRecords.Remove(record);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
