using PhotographySessionLogger.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PhotographySessionLogger.Api.Features.Users;

public record GetUserByIdQuery : IRequest<UserDto?>
{
    public Guid UserId { get; init; }
}

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    private readonly IPhotographySessionLoggerContext _context;
    private readonly ILogger<GetUserByIdQueryHandler> _logger;

    public GetUserByIdQueryHandler(IPhotographySessionLoggerContext context, ILogger<GetUserByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting user {UserId}", request.UserId);
        var user = await _context.Users.Include(u => u.UserRoles).AsNoTracking().FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);
        if (user == null) return null;
        var roles = await _context.Roles.AsNoTracking().ToListAsync(cancellationToken);
        return user.ToDto(roles);
    }
}
