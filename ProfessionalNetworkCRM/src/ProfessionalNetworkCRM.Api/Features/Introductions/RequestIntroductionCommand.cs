using ProfessionalNetworkCRM.Core;
using ProfessionalNetworkCRM.Core.Models.IntroductionAggregate;
using ProfessionalNetworkCRM.Core.Models.IntroductionAggregate.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ProfessionalNetworkCRM.Api.Features.Introductions;

public record RequestIntroductionCommand : IRequest<IntroductionDto>
{
    public Guid FromContactId { get; init; }
    public Guid ToContactId { get; init; }
    public string Purpose { get; init; } = string.Empty;
    public string? Notes { get; init; }
}

public class RequestIntroductionCommandHandler : IRequestHandler<RequestIntroductionCommand, IntroductionDto>
{
    private readonly IProfessionalNetworkCRMContext _context;
    private readonly ILogger<RequestIntroductionCommandHandler> _logger;
    private readonly ITenantContext _tenantContext;

    public RequestIntroductionCommandHandler(
        IProfessionalNetworkCRMContext context,
        ILogger<RequestIntroductionCommandHandler> logger,
        ITenantContext tenantContext)
    {
        _context = context;
        _logger = logger;
        _tenantContext = tenantContext;
    }

    public async Task<IntroductionDto> Handle(RequestIntroductionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Requesting introduction from {FromContactId} to {ToContactId}",
            request.FromContactId,
            request.ToContactId);

        var introduction = new Introduction
        {
            IntroductionId = Guid.NewGuid(),
            FromContactId = request.FromContactId,
            ToContactId = request.ToContactId,
            TenantId = _tenantContext.TenantId,
            Purpose = request.Purpose,
            Status = IntroductionStatus.Requested,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Introductions.Add(introduction);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created introduction request {IntroductionId}",
            introduction.IntroductionId);

        return introduction.ToDto();
    }
}
