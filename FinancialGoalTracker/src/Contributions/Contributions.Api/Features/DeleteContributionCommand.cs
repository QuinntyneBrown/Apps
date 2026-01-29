using Contributions.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Contributions.Api.Features;

public record DeleteContributionCommand : IRequest<bool>
{
    public Guid ContributionId { get; init; }
}

public class DeleteContributionCommandHandler : IRequestHandler<DeleteContributionCommand, bool>
{
    private readonly IContributionsDbContext _context;
    private readonly ILogger<DeleteContributionCommandHandler> _logger;

    public DeleteContributionCommandHandler(IContributionsDbContext context, ILogger<DeleteContributionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteContributionCommand request, CancellationToken cancellationToken)
    {
        var contribution = await _context.Contributions
            .FirstOrDefaultAsync(c => c.ContributionId == request.ContributionId, cancellationToken);

        if (contribution == null) return false;

        _context.Contributions.Remove(contribution);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Contribution deleted: {ContributionId}", request.ContributionId);

        return true;
    }
}
