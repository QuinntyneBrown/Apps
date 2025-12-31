using MeetingNotesActionItemTracker.Core.Model.UserAggregate;
using MeetingNotesActionItemTracker.Core.Model.UserAggregate.Entities;

namespace MeetingNotesActionItemTracker.Core.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user, IEnumerable<Role> roles);
    DateTime GetTokenExpiration();
}
