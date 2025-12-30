// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HydrationTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HydrationTracker.Api.Features.Reminder;

public record GetRemindersQuery() : IRequest<List<ReminderDto>>;

public class GetRemindersQueryHandler : IRequestHandler<GetRemindersQuery, List<ReminderDto>>
{
    private readonly IHydrationTrackerContext _context;

    public GetRemindersQueryHandler(IHydrationTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<ReminderDto>> Handle(GetRemindersQuery request, CancellationToken cancellationToken)
    {
        return await _context.Reminders
            .AsNoTracking()
            .Select(x => x.ToDto())
            .ToListAsync(cancellationToken);
    }
}
