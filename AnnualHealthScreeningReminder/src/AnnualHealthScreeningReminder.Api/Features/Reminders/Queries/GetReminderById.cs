// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnnualHealthScreeningReminder.Api.Features.Reminders.DTOs;
using AnnualHealthScreeningReminder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AnnualHealthScreeningReminder.Api.Features.Reminders.Queries;

public class GetReminderById
{
    public record Query(Guid ReminderId) : IRequest<ReminderDto>;

    public class Handler : IRequestHandler<Query, ReminderDto>
    {
        private readonly IAnnualHealthScreeningReminderContext _context;

        public Handler(IAnnualHealthScreeningReminderContext context)
        {
            _context = context;
        }

        public async Task<ReminderDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var reminder = await _context.Reminders
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ReminderId == request.ReminderId, cancellationToken)
                ?? throw new KeyNotFoundException($"Reminder with ID {request.ReminderId} not found.");

            return new ReminderDto
            {
                ReminderId = reminder.ReminderId,
                UserId = reminder.UserId,
                ScreeningId = reminder.ScreeningId,
                ReminderDate = reminder.ReminderDate,
                Message = reminder.Message,
                IsSent = reminder.IsSent,
                CreatedAt = reminder.CreatedAt
            };
        }
    }
}
