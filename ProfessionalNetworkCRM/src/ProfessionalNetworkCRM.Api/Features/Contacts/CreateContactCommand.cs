using ProfessionalNetworkCRM.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ProfessionalNetworkCRM.Api.Features.Contacts;

public record CreateContactCommand : IRequest<ContactDto>
{
    public Guid UserId { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public ContactType ContactType { get; init; }
    public string? Company { get; init; }
    public string? JobTitle { get; init; }
    public string? Email { get; init; }
    public string? Phone { get; init; }
    public string? LinkedInUrl { get; init; }
    public string? Location { get; init; }
    public string? Notes { get; init; }
    public List<string>? Tags { get; init; }
    public DateTime? DateMet { get; init; }
    public bool IsPriority { get; init; }
}

public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, ContactDto>
{
    private readonly IProfessionalNetworkCRMContext _context;
    private readonly ILogger<CreateContactCommandHandler> _logger;

    public CreateContactCommandHandler(
        IProfessionalNetworkCRMContext context,
        ILogger<CreateContactCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ContactDto> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating contact for user {UserId}, name: {FirstName} {LastName}",
            request.UserId,
            request.FirstName,
            request.LastName);

        var contact = new Contact
        {
            ContactId = Guid.NewGuid(),
            UserId = request.UserId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            ContactType = request.ContactType,
            Company = request.Company,
            JobTitle = request.JobTitle,
            Email = request.Email,
            Phone = request.Phone,
            LinkedInUrl = request.LinkedInUrl,
            Location = request.Location,
            Notes = request.Notes,
            Tags = request.Tags ?? new List<string>(),
            DateMet = request.DateMet ?? DateTime.UtcNow,
            IsPriority = request.IsPriority,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Contacts.Add(contact);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created contact {ContactId} for user {UserId}",
            contact.ContactId,
            request.UserId);

        return contact.ToDto();
    }
}
