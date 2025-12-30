// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnnualHealthScreeningReminder.Api.Features.Reminders.DTOs;
using AnnualHealthScreeningReminder.Core;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AnnualHealthScreeningReminder.Api.Features.Reminders.Commands;

public class MarkReminderAsSent
{
    public record Command(Guid ReminderId) : IRequest<ReminderDto>;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.ReminderId).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Command, ReminderDto>
    {
        private readonly IAnnualHealthScreeningReminderContext _context;
        private readonly IValidator<Command> _validator;

        public Handler(IAnnualHealthScreeningReminderContext context, IValidator<Command> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<ReminderDto> Handle(Command request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var reminder = await _context.Reminders
                .FirstOrDefaultAsync(x => x.ReminderId == request.ReminderId, cancellationToken)
                ?? throw new KeyNotFoundException($"Reminder with ID {request.ReminderId} not found.");

            reminder.MarkAsSent();

            await _context.SaveChangesAsync(cancellationToken);

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
