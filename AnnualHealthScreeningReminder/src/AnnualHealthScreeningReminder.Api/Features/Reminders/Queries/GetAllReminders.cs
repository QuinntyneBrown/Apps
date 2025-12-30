// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnnualHealthScreeningReminder.Api.Features.Reminders.DTOs;
using AnnualHealthScreeningReminder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AnnualHealthScreeningReminder.Api.Features.Reminders.Queries;

public class GetAllReminders
{
    public record Query(Guid? UserId = null, Guid? ScreeningId = null, bool? IsSent = null) : IRequest<List<ReminderDto>>;

    public class Handler : IRequestHandler<Query, List<ReminderDto>>
    {
        private readonly IAnnualHealthScreeningReminderContext _context;

        public Handler(IAnnualHealthScreeningReminderContext context)
        {
            _context = context;
        }

        public async Task<List<ReminderDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _context.Reminders.AsNoTracking();

            if (request.UserId.HasValue)
            {
                query = query.Where(x => x.UserId == request.UserId.Value);
            }

            if (request.ScreeningId.HasValue)
            {
                query = query.Where(x => x.ScreeningId == request.ScreeningId.Value);
            }

            if (request.IsSent.HasValue)
            {
                query = query.Where(x => x.IsSent == request.IsSent.Value);
            }

            var reminders = await query
                .OrderByDescending(x => x.ReminderDate)
                .ToListAsync(cancellationToken);

            return reminders.Select(reminder => new ReminderDto
            {
                ReminderId = reminder.ReminderId,
                UserId = reminder.UserId,
                ScreeningId = reminder.ScreeningId,
                ReminderDate = reminder.ReminderDate,
                Message = reminder.Message,
                IsSent = reminder.IsSent,
                CreatedAt = reminder.CreatedAt
            }).ToList();
        }
    }
}
