// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ChoreAssignmentTracker.Core;
using MediatR;

namespace ChoreAssignmentTracker.Api.Features.FamilyMembers;

/// <summary>
/// Command to create a family member.
/// </summary>
public class CreateFamilyMember : IRequest<FamilyMemberDto>
{
    /// <summary>
    /// Gets or sets the family member data.
    /// </summary>
    public CreateFamilyMemberDto FamilyMember { get; set; } = null!;
}

/// <summary>
/// Handler for CreateFamilyMember command.
/// </summary>
public class CreateFamilyMemberHandler : IRequestHandler<CreateFamilyMember, FamilyMemberDto>
{
    private readonly IChoreAssignmentTrackerContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateFamilyMemberHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CreateFamilyMemberHandler(IChoreAssignmentTrackerContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Handles the CreateFamilyMember command.
    /// </summary>
    /// <param name="request">The command request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The created family member DTO.</returns>
    public async Task<FamilyMemberDto> Handle(CreateFamilyMember request, CancellationToken cancellationToken)
    {
        var familyMember = new FamilyMember
        {
            FamilyMemberId = Guid.NewGuid(),
            UserId = request.FamilyMember.UserId,
            Name = request.FamilyMember.Name,
            Age = request.FamilyMember.Age,
            Avatar = request.FamilyMember.Avatar,
            TotalPoints = 0,
            AvailablePoints = 0,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        _context.FamilyMembers.Add(familyMember);
        await _context.SaveChangesAsync(cancellationToken);

        return new FamilyMemberDto
        {
            FamilyMemberId = familyMember.FamilyMemberId,
            UserId = familyMember.UserId,
            Name = familyMember.Name,
            Age = familyMember.Age,
            Avatar = familyMember.Avatar,
            TotalPoints = familyMember.TotalPoints,
            AvailablePoints = familyMember.AvailablePoints,
            IsActive = familyMember.IsActive,
            CreatedAt = familyMember.CreatedAt,
            CompletionRate = 0
        };
    }
}
