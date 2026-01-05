using ProfessionalNetworkCRM.Core;
using ProfessionalNetworkCRM.Core.Models.IntroductionAggregate.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProfessionalNetworkCRM.Api.Features.Introductions;

public record MakeIntroductionCommand : IRequest<IntroductionDto?>
{
    public Guid IntroductionId { get; init; }
    public string? Notes { get; init; }
}

public class MakeIntroductionCommandHandler : IRequestHandler<MakeIntroductionCommand, IntroductionDto?>
{
    private readonly IProfessionalNetworkCRMContext _context;
    private readonly ILogger<MakeIntroductionCommandHandler> _logger;

    public MakeIntroductionCommandHandler(
        IProfessionalNetworkCRMContext context,
        ILogger<MakeIntroductionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IntroductionDto?> Handle(MakeIntroductionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Making introduction {IntroductionId}", request.IntroductionId);

        var introduction = await _context.Introductions
            .FirstOrDefaultAsync(i => i.IntroductionId == request.IntroductionId, cancellationToken);

        if (introduction == null)
        {
            _logger.LogWarning("Introduction {IntroductionId} not found", request.IntroductionId);
            return null;
        }

        introduction.UpdateStatus(IntroductionStatus.Made);
        if (!string.IsNullOrEmpty(request.Notes))
        {
            introduction.Notes = request.Notes;
        }

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Introduction {IntroductionId} marked as made", request.IntroductionId);

        return introduction.ToDto();
    }
}
