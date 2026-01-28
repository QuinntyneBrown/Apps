using Intakes.Core;
using Intakes.Core.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Intakes.Api.Features;

public record UpdateIntakeCommand(
    Guid IntakeId,
    BeverageType BeverageType,
    decimal AmountMl,
    DateTime LoggedAt,
    string? Notes) : IRequest<IntakeDto?>;

public class UpdateIntakeCommandHandler : IRequestHandler<UpdateIntakeCommand, IntakeDto?>
{
    private readonly IIntakesDbContext _context;

    public UpdateIntakeCommandHandler(IIntakesDbContext context)
    {
        _context = context;
    }

    public async Task<IntakeDto?> Handle(UpdateIntakeCommand request, CancellationToken cancellationToken)
    {
        var intake = await _context.Intakes
            .FirstOrDefaultAsync(i => i.IntakeId == request.IntakeId, cancellationToken);

        if (intake == null) return null;

        intake.BeverageType = request.BeverageType;
        intake.AmountMl = request.AmountMl;
        intake.LoggedAt = request.LoggedAt;
        intake.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        return intake.ToDto();
    }
}
