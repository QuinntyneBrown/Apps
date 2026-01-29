using Identity.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Features.Users;

public record GetUserByIdQuery(Guid UserId) : IRequest<UserDto?>;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    private readonly IIdentityDbContext _context;

    public GetUserByIdQueryHandler(IIdentityDbContext context)
    {
        _context = context;
    }

    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);

        return user?.ToDto();
    }
}
