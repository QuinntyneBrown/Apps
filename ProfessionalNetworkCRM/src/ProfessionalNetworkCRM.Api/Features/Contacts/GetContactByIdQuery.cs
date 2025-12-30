using ProfessionalNetworkCRM.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProfessionalNetworkCRM.Api.Features.Contacts;

public record GetContactByIdQuery : IRequest<ContactDto?>
{
    public Guid ContactId { get; init; }
}

public class GetContactByIdQueryHandler : IRequestHandler<GetContactByIdQuery, ContactDto?>
{
    private readonly IProfessionalNetworkCRMContext _context;
    private readonly ILogger<GetContactByIdQueryHandler> _logger;

    public GetContactByIdQueryHandler(
        IProfessionalNetworkCRMContext context,
        ILogger<GetContactByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ContactDto?> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting contact {ContactId}", request.ContactId);

        var contact = await _context.Contacts
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.ContactId == request.ContactId, cancellationToken);

        if (contact == null)
        {
            _logger.LogWarning("Contact {ContactId} not found", request.ContactId);
            return null;
        }

        return contact.ToDto();
    }
}
