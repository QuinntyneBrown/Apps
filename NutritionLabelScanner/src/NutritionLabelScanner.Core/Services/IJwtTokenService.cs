using NutritionLabelScanner.Core.Models.UserAggregate;
using NutritionLabelScanner.Core.Models.UserAggregate.Entities;

namespace NutritionLabelScanner.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
