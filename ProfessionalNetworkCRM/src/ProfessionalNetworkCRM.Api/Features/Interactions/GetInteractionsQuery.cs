using ProfessionalNetworkCRM.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProfessionalNetworkCRM.Api.Features.Interactions;

public record GetInteractionsQuery : IRequest<IEnumerable<InteractionDto>>
{
    public Guid? UserId { get; init; }
    public Guid? ContactId { get; init; }
    public string? InteractionType { get; init; }
}

public class GetInteractionsQueryHandler : IRequestHandler<GetInteractionsQuery, IEnumerable<InteractionDto>>
{
    private readonly IProfessionalNetworkCRMContext _context;
    private readonly ILogger<GetInteractionsQueryHandler> _logger;

    public GetInteractionsQueryHandler(
        IProfessionalNetworkCRMContext context,
        ILogger<GetInteractionsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<InteractionDto>> Handle(GetInteractionsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting interactions for user {UserId}, contact {ContactId}", request.UserId, request.ContactId);

        var query = _context.Interactions.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(i => i.UserId == request.UserId.Value);
        }

        if (request.ContactId.HasValue)
        {
            query = query.Where(i => i.ContactId == request.ContactId.Value);
        }

        if (!string.IsNullOrEmpty(request.InteractionType))
        {
            query = query.Where(i => i.InteractionType == request.InteractionType);
        }

        var interactions = await query
            .OrderByDescending(i => i.InteractionDate)
            .ToListAsync(cancellationToken);

        return interactions.Select(i => i.ToDto());
    }
}
