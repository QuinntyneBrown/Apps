using Manuals.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Manuals.Api.Features;

public record UpdateManualCommand(
    Guid ManualId,
    string? Title,
    string? FileUrl,
    string? FileType) : IRequest<ManualDto?>;

public class UpdateManualCommandHandler : IRequestHandler<UpdateManualCommand, ManualDto?>
{
    private readonly IManualsDbContext _context;

    public UpdateManualCommandHandler(IManualsDbContext context)
    {
        _context = context;
    }

    public async Task<ManualDto?> Handle(UpdateManualCommand request, CancellationToken cancellationToken)
    {
        var manual = await _context.Manuals
            .FirstOrDefaultAsync(m => m.ManualId == request.ManualId, cancellationToken);

        if (manual == null) return null;

        manual.Title = request.Title;
        manual.FileUrl = request.FileUrl;
        manual.FileType = request.FileType;

        await _context.SaveChangesAsync(cancellationToken);
        return manual.ToDto();
    }
}
