using Intakes.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Intakes.Api.Features;

public record DeleteIntakeCommand(Guid IntakeId) : IRequest<bool>;

public class DeleteIntakeCommandHandler : IRequestHandler<DeleteIntakeCommand, bool>
{
    private readonly IIntakesDbContext _context;

    public DeleteIntakeCommandHandler(IIntakesDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteIntakeCommand request, CancellationToken cancellationToken)
    {
        var intake = await _context.Intakes
            .FirstOrDefaultAsync(i => i.IntakeId == request.IntakeId, cancellationToken);

        if (intake == null) return false;

        _context.Intakes.Remove(intake);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
