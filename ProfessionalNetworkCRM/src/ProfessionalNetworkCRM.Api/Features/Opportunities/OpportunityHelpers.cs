namespace ProfessionalNetworkCRM.Api.Features.Opportunities;

public static class OpportunityHelpers
{
    public static decimal? ParseValue(string? potentialValue)
    {
        if (string.IsNullOrEmpty(potentialValue))
        {
            return null;
        }

        if (decimal.TryParse(potentialValue, out var parsedValue))
        {
            return parsedValue;
        }

        return null;
    }
}
