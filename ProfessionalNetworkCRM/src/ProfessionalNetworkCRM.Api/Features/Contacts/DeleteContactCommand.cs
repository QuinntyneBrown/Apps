using ProfessionalNetworkCRM.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProfessionalNetworkCRM.Api.Features.Contacts;

public record DeleteContactCommand : IRequest<bool>
{
    public Guid ContactId { get; init; }
}

public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, bool>
{
    private readonly IProfessionalNetworkCRMContext _context;
    private readonly ILogger<DeleteContactCommandHandler> _logger;

    public DeleteContactCommandHandler(
        IProfessionalNetworkCRMContext context,
        ILogger<DeleteContactCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting contact {ContactId}", request.ContactId);

        var contact = await _context.Contacts
            .FirstOrDefaultAsync(c => c.ContactId == request.ContactId, cancellationToken);

        if (contact == null)
        {
            _logger.LogWarning("Contact {ContactId} not found", request.ContactId);
            return false;
        }

        _context.Contacts.Remove(contact);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted contact {ContactId}", request.ContactId);

        return true;
    }
}
