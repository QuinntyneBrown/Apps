using MediatR;
using Microsoft.EntityFrameworkCore;
using Runs.Core;

namespace Runs.Api.Features;

public record DeleteRunCommand(Guid RunId) : IRequest<bool>;

public class DeleteRunCommandHandler : IRequestHandler<DeleteRunCommand, bool>
{
    private readonly IRunsDbContext _context;
    public DeleteRunCommandHandler(IRunsDbContext context) => _context = context;

    public async Task<bool> Handle(DeleteRunCommand request, CancellationToken cancellationToken)
    {
        var run = await _context.Runs.FirstOrDefaultAsync(r => r.RunId == request.RunId, cancellationToken);
        if (run == null) return false;
        _context.Runs.Remove(run);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
