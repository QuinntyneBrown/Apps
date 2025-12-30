using ProfessionalNetworkCRM.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ProfessionalNetworkCRM.Api.Features.FollowUps;

public record CreateFollowUpCommand : IRequest<FollowUpDto>
{
    public Guid UserId { get; init; }
    public Guid ContactId { get; init; }
    public string Description { get; init; } = string.Empty;
    public DateTime DueDate { get; init; }
    public string Priority { get; init; } = "Medium";
    public string? Notes { get; init; }
}

public class CreateFollowUpCommandHandler : IRequestHandler<CreateFollowUpCommand, FollowUpDto>
{
    private readonly IProfessionalNetworkCRMContext _context;
    private readonly ILogger<CreateFollowUpCommandHandler> _logger;

    public CreateFollowUpCommandHandler(
        IProfessionalNetworkCRMContext context,
        ILogger<CreateFollowUpCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<FollowUpDto> Handle(CreateFollowUpCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating follow-up for contact {ContactId}, due: {DueDate}",
            request.ContactId,
            request.DueDate);

        var followUp = new FollowUp
        {
            FollowUpId = Guid.NewGuid(),
            UserId = request.UserId,
            ContactId = request.ContactId,
            Description = request.Description,
            DueDate = request.DueDate,
            Priority = request.Priority,
            Notes = request.Notes,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.FollowUps.Add(followUp);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created follow-up {FollowUpId} for contact {ContactId}",
            followUp.FollowUpId,
            request.ContactId);

        return followUp.ToDto();
    }
}
