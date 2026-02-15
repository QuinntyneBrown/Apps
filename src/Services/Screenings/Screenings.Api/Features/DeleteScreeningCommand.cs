using Screenings.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Screenings.Api.Features;

public record DeleteScreeningCommand(Guid ScreeningId) : IRequest<bool>;

public class DeleteScreeningCommandHandler : IRequestHandler<DeleteScreeningCommand, bool>
{
    private readonly IScreeningsDbContext _context;
    private readonly ILogger<DeleteScreeningCommandHandler> _logger;

    public DeleteScreeningCommandHandler(IScreeningsDbContext context, ILogger<DeleteScreeningCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteScreeningCommand request, CancellationToken cancellationToken)
    {
        var screening = await _context.Screenings
            .FirstOrDefaultAsync(s => s.ScreeningId == request.ScreeningId, cancellationToken);

        if (screening == null) return false;

        _context.Screenings.Remove(screening);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Screening deleted: {ScreeningId}", request.ScreeningId);
        return true;
    }
}
