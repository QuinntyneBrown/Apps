using Applications.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Applications.Api.Features;

public record DeleteApplicationCommand(Guid ApplicationId) : IRequest<bool>;

public class DeleteApplicationCommandHandler : IRequestHandler<DeleteApplicationCommand, bool>
{
    private readonly IApplicationsDbContext _context;
    private readonly ILogger<DeleteApplicationCommandHandler> _logger;

    public DeleteApplicationCommandHandler(IApplicationsDbContext context, ILogger<DeleteApplicationCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteApplicationCommand request, CancellationToken cancellationToken)
    {
        var application = await _context.Applications
            .FirstOrDefaultAsync(a => a.ApplicationId == request.ApplicationId, cancellationToken);

        if (application == null) return false;

        _context.Applications.Remove(application);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Application deleted: {ApplicationId}", request.ApplicationId);

        return true;
    }
}
