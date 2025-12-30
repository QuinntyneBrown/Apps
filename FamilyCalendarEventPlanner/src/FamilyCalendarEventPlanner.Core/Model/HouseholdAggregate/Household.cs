using FamilyCalendarEventPlanner.Core.Model.HouseholdAggregate.Enums;

namespace FamilyCalendarEventPlanner.Core.Model.HouseholdAggregate;

public class Household
{
    public Guid HouseholdId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Street { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public CanadianProvince Province { get; private set; }
    public string PostalCode { get; private set; } = string.Empty;

    private Household()
    {
    }

    public Household(
        string name,
        string street,
        string city,
        CanadianProvince province,
        string postalCode)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name cannot be empty.", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(street))
        {
            throw new ArgumentException("Street cannot be empty.", nameof(street));
        }

        if (string.IsNullOrWhiteSpace(city))
        {
            throw new ArgumentException("City cannot be empty.", nameof(city));
        }

        if (string.IsNullOrWhiteSpace(postalCode))
        {
            throw new ArgumentException("Postal code cannot be empty.", nameof(postalCode));
        }

        if (!IsValidCanadianPostalCode(postalCode))
        {
            throw new ArgumentException("Invalid Canadian postal code format.", nameof(postalCode));
        }

        HouseholdId = Guid.NewGuid();
        Name = name;
        Street = street;
        City = city;
        Province = province;
        PostalCode = postalCode.ToUpperInvariant().Replace(" ", "");
    }

    public void Update(
        string? name = null,
        string? street = null,
        string? city = null,
        CanadianProvince? province = null,
        string? postalCode = null)
    {
        if (name != null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be empty.", nameof(name));
            }
            Name = name;
        }

        if (street != null)
        {
            if (string.IsNullOrWhiteSpace(street))
            {
                throw new ArgumentException("Street cannot be empty.", nameof(street));
            }
            Street = street;
        }

        if (city != null)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentException("City cannot be empty.", nameof(city));
            }
            City = city;
        }

        if (province.HasValue)
        {
            Province = province.Value;
        }

        if (postalCode != null)
        {
            if (string.IsNullOrWhiteSpace(postalCode))
            {
                throw new ArgumentException("Postal code cannot be empty.", nameof(postalCode));
            }

            if (!IsValidCanadianPostalCode(postalCode))
            {
                throw new ArgumentException("Invalid Canadian postal code format.", nameof(postalCode));
            }

            PostalCode = postalCode.ToUpperInvariant().Replace(" ", "");
        }
    }

    public string GetFormattedPostalCode()
    {
        if (PostalCode.Length == 6)
        {
            return $"{PostalCode.Substring(0, 3)} {PostalCode.Substring(3, 3)}";
        }
        return PostalCode;
    }

    public string GetFullAddress()
    {
        return $"{Street}, {City}, {Province} {GetFormattedPostalCode()}";
    }

    private static bool IsValidCanadianPostalCode(string postalCode)
    {
        var normalized = postalCode.ToUpperInvariant().Replace(" ", "");
        if (normalized.Length != 6)
        {
            return false;
        }

        return char.IsLetter(normalized[0]) &&
               char.IsDigit(normalized[1]) &&
               char.IsLetter(normalized[2]) &&
               char.IsDigit(normalized[3]) &&
               char.IsLetter(normalized[4]) &&
               char.IsDigit(normalized[5]);
    }
}
