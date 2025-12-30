// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Core;
using MediatR;

namespace FriendGroupEventCoordinator.Api.Features.Members;

/// <summary>
/// Command to create a new member.
/// </summary>
public record CreateMemberCommand(CreateMemberDto Member) : IRequest<MemberDto>;

/// <summary>
/// Handler for creating a new member.
/// </summary>
public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, MemberDto>
{
    private readonly IFriendGroupEventCoordinatorContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateMemberCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CreateMemberCommandHandler(IFriendGroupEventCoordinatorContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Handles the create member command.
    /// </summary>
    /// <param name="request">The command request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created member DTO.</returns>
    public async Task<MemberDto> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        var member = new Member
        {
            MemberId = Guid.NewGuid(),
            GroupId = request.Member.GroupId,
            UserId = request.Member.UserId,
            Name = request.Member.Name,
            Email = request.Member.Email,
            IsAdmin = request.Member.IsAdmin,
            IsActive = true,
            JoinedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };

        _context.Members.Add(member);
        await _context.SaveChangesAsync(cancellationToken);

        return new MemberDto
        {
            MemberId = member.MemberId,
            GroupId = member.GroupId,
            UserId = member.UserId,
            Name = member.Name,
            Email = member.Email,
            IsAdmin = member.IsAdmin,
            IsActive = member.IsActive,
            JoinedAt = member.JoinedAt,
            CreatedAt = member.CreatedAt,
            UpdatedAt = member.UpdatedAt
        };
    }
}
