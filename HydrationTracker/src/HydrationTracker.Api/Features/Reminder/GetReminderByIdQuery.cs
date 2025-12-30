// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HydrationTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HydrationTracker.Api.Features.Reminder;

public record GetReminderByIdQuery(Guid ReminderId) : IRequest<ReminderDto>;

public class GetReminderByIdQueryHandler : IRequestHandler<GetReminderByIdQuery, ReminderDto>
{
    private readonly IHydrationTrackerContext _context;

    public GetReminderByIdQueryHandler(IHydrationTrackerContext context)
    {
        _context = context;
    }

    public async Task<ReminderDto> Handle(GetReminderByIdQuery request, CancellationToken cancellationToken)
    {
        var reminder = await _context.Reminders
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ReminderId == request.ReminderId, cancellationToken)
            ?? throw new InvalidOperationException($"Reminder with ID {request.ReminderId} not found.");

        return reminder.ToDto();
    }
}
