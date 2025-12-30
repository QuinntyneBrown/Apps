using FamilyCalendarEventPlanner.Core;
using FamilyCalendarEventPlanner.Core.Model.HouseholdAggregate;
using FamilyCalendarEventPlanner.Core.Model.HouseholdAggregate.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FamilyCalendarEventPlanner.Api.Features.Households;

public record CreateHouseholdCommand : IRequest<HouseholdDto>
{
    public string Name { get; init; } = string.Empty;
    public string Street { get; init; } = string.Empty;
    public string City { get; init; } = string.Empty;
    public CanadianProvince Province { get; init; }
    public string PostalCode { get; init; } = string.Empty;
}

public class CreateHouseholdCommandHandler : IRequestHandler<CreateHouseholdCommand, HouseholdDto>
{
    private readonly IFamilyCalendarEventPlannerContext _context;
    private readonly ILogger<CreateHouseholdCommandHandler> _logger;

    public CreateHouseholdCommandHandler(
        IFamilyCalendarEventPlannerContext context,
        ILogger<CreateHouseholdCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<HouseholdDto> Handle(CreateHouseholdCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating household with name: {Name}, city: {City}, province: {Province}",
            request.Name,
            request.City,
            request.Province);

        var household = new Household(
            request.Name,
            request.Street,
            request.City,
            request.Province,
            request.PostalCode);

        _context.Households.Add(household);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created household {HouseholdId} with name: {Name}",
            household.HouseholdId,
            request.Name);

        return household.ToDto();
    }
}
