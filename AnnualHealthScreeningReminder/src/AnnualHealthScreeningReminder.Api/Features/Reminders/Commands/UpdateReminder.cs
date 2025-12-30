// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnnualHealthScreeningReminder.Api.Features.Reminders.DTOs;
using AnnualHealthScreeningReminder.Core;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AnnualHealthScreeningReminder.Api.Features.Reminders.Commands;

public class UpdateReminder
{
    public record Command(
        Guid ReminderId,
        DateTime ReminderDate,
        string? Message) : IRequest<ReminderDto>;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.ReminderId).NotEmpty();
            RuleFor(x => x.ReminderDate).NotEmpty();
            RuleFor(x => x.Message).MaximumLength(1000);
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

            reminder.ReminderDate = request.ReminderDate;
            reminder.Message = request.Message;

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
