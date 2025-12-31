using FocusSessionTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FocusSessionTracker.Api.Features.Users;

public record DeleteUserCommand : IRequest<bool>
{
    public Guid UserId { get; init; }
}

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IFocusSessionTrackerContext _context;
    private readonly ILogger<DeleteUserCommandHandler> _logger;

    public DeleteUserCommandHandler(IFocusSessionTrackerContext context, ILogger<DeleteUserCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting user {UserId}", request.UserId);
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);
        if (user == null) return false;
        _context.Users.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
