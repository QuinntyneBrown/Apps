using Screenings.Core;
using Screenings.Core.Models;
using MediatR;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using Messaging.Contracts.Events;

namespace Screenings.Api.Features;

public record CreateScreeningCommand(
    Guid UserId,
    Guid TenantId,
    string ScreeningType,
    DateTime ScheduledDate,
    string? Notes,
    int FrequencyMonths) : IRequest<ScreeningDto>;

public class CreateScreeningCommandHandler : IRequestHandler<CreateScreeningCommand, ScreeningDto>
{
    private readonly IScreeningsDbContext _context;
    private readonly ILogger<CreateScreeningCommandHandler> _logger;
    private readonly IConnection? _rabbitConnection;

    public CreateScreeningCommandHandler(
        IScreeningsDbContext context,
        ILogger<CreateScreeningCommandHandler> logger,
        IConnection? rabbitConnection = null)
    {
        _context = context;
        _logger = logger;
        _rabbitConnection = rabbitConnection;
    }

    public async Task<ScreeningDto> Handle(CreateScreeningCommand request, CancellationToken cancellationToken)
    {
        var screening = new Screening
        {
            ScreeningId = Guid.NewGuid(),
            UserId = request.UserId,
            TenantId = request.TenantId,
            ScreeningType = request.ScreeningType,
            ScheduledDate = request.ScheduledDate,
            Notes = request.Notes,
            FrequencyMonths = request.FrequencyMonths,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.Screenings.Add(screening);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Screening created: {ScreeningId}", screening.ScreeningId);

        return screening.ToDto();
    }
}
