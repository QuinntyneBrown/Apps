using Contributions.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Contributions.Api.Features;

public record DeleteContributionCommand(Guid ContributionId) : IRequest<bool>;

public class DeleteContributionCommandHandler : IRequestHandler<DeleteContributionCommand, bool>
{
    private readonly IContributionsDbContext _context;
    public DeleteContributionCommandHandler(IContributionsDbContext context) => _context = context;
    public async Task<bool> Handle(DeleteContributionCommand request, CancellationToken ct)
    {
        var contribution = await _context.Contributions.FirstOrDefaultAsync(c => c.ContributionId == request.ContributionId, ct);
        if (contribution == null) return false;
        _context.Contributions.Remove(contribution);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}
