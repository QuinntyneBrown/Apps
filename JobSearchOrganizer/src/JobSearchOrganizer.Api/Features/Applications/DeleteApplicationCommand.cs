using JobSearchOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JobSearchOrganizer.Api.Features.Applications;

public record DeleteApplicationCommand : IRequest<bool>
{
    public Guid ApplicationId { get; init; }
}

public class DeleteApplicationCommandHandler : IRequestHandler<DeleteApplicationCommand, bool>
{
    private readonly IJobSearchOrganizerContext _context;
    private readonly ILogger<DeleteApplicationCommandHandler> _logger;

    public DeleteApplicationCommandHandler(
        IJobSearchOrganizerContext context,
        ILogger<DeleteApplicationCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteApplicationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting application {ApplicationId}", request.ApplicationId);

        var application = await _context.Applications
            .FirstOrDefaultAsync(a => a.ApplicationId == request.ApplicationId, cancellationToken);

        if (application == null)
        {
            _logger.LogWarning("Application {ApplicationId} not found", request.ApplicationId);
            return false;
        }

        _context.Applications.Remove(application);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted application {ApplicationId}", request.ApplicationId);

        return true;
    }
}
