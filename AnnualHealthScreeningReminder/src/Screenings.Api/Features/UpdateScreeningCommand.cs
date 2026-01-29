using Screenings.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Screenings.Api.Features;

public record UpdateScreeningCommand(
    Guid ScreeningId,
    string ScreeningType,
    DateTime ScheduledDate,
    DateTime? CompletedDate,
    string? Result,
    string? Notes,
    int FrequencyMonths,
    bool IsCompleted) : IRequest<ScreeningDto?>;

public class UpdateScreeningCommandHandler : IRequestHandler<UpdateScreeningCommand, ScreeningDto?>
{
    private readonly IScreeningsDbContext _context;
    private readonly ILogger<UpdateScreeningCommandHandler> _logger;

    public UpdateScreeningCommandHandler(IScreeningsDbContext context, ILogger<UpdateScreeningCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ScreeningDto?> Handle(UpdateScreeningCommand request, CancellationToken cancellationToken)
    {
        var screening = await _context.Screenings
            .FirstOrDefaultAsync(s => s.ScreeningId == request.ScreeningId, cancellationToken);

        if (screening == null) return null;

        screening.ScreeningType = request.ScreeningType;
        screening.ScheduledDate = request.ScheduledDate;
        screening.CompletedDate = request.CompletedDate;
        screening.Result = request.Result;
        screening.Notes = request.Notes;
        screening.FrequencyMonths = request.FrequencyMonths;
        screening.IsCompleted = request.IsCompleted;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Screening updated: {ScreeningId}", screening.ScreeningId);

        return screening.ToDto();
    }
}
