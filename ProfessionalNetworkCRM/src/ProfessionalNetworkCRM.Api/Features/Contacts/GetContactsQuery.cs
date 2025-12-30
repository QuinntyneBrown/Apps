using ProfessionalNetworkCRM.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProfessionalNetworkCRM.Api.Features.Contacts;

public record GetContactsQuery : IRequest<IEnumerable<ContactDto>>
{
    public Guid? UserId { get; init; }
    public ContactType? ContactType { get; init; }
    public bool? IsPriority { get; init; }
    public string? Tag { get; init; }
}

public class GetContactsQueryHandler : IRequestHandler<GetContactsQuery, IEnumerable<ContactDto>>
{
    private readonly IProfessionalNetworkCRMContext _context;
    private readonly ILogger<GetContactsQueryHandler> _logger;

    public GetContactsQueryHandler(
        IProfessionalNetworkCRMContext context,
        ILogger<GetContactsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ContactDto>> Handle(GetContactsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting contacts for user {UserId}", request.UserId);

        var query = _context.Contacts.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(c => c.UserId == request.UserId.Value);
        }

        if (request.ContactType.HasValue)
        {
            query = query.Where(c => c.ContactType == request.ContactType.Value);
        }

        if (request.IsPriority.HasValue)
        {
            query = query.Where(c => c.IsPriority == request.IsPriority.Value);
        }

        if (!string.IsNullOrEmpty(request.Tag))
        {
            query = query.Where(c => c.Tags.Contains(request.Tag));
        }

        var contacts = await query
            .OrderByDescending(c => c.LastContactedDate ?? c.DateMet)
            .ToListAsync(cancellationToken);

        return contacts.Select(c => c.ToDto());
    }
}
