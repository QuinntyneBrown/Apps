// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnnualHealthScreeningReminder.Core;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AnnualHealthScreeningReminder.Api.Features.Appointments.Commands;

public class DeleteAppointment
{
    public record Command(Guid AppointmentId) : IRequest<Unit>;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.AppointmentId).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IAnnualHealthScreeningReminderContext _context;
        private readonly IValidator<Command> _validator;

        public Handler(IAnnualHealthScreeningReminderContext context, IValidator<Command> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(x => x.AppointmentId == request.AppointmentId, cancellationToken)
                ?? throw new KeyNotFoundException($"Appointment with ID {request.AppointmentId} not found.");

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
