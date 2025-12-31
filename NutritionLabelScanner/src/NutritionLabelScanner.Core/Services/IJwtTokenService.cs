using NutritionLabelScanner.Core.Model.UserAggregate;
using NutritionLabelScanner.Core.Model.UserAggregate.Entities;

namespace NutritionLabelScanner.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
