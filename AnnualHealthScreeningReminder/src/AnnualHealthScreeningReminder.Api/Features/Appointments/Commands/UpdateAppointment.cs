// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnnualHealthScreeningReminder.Api.Features.Appointments.DTOs;
using AnnualHealthScreeningReminder.Core;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AnnualHealthScreeningReminder.Api.Features.Appointments.Commands;

public class UpdateAppointment
{
    public record Command(
        Guid AppointmentId,
        DateTime AppointmentDate,
        string Location,
        string? Provider,
        bool IsCompleted,
        string? Notes) : IRequest<AppointmentDto>;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.AppointmentId).NotEmpty();
            RuleFor(x => x.AppointmentDate).NotEmpty();
            RuleFor(x => x.Location).NotEmpty().MaximumLength(500);
            RuleFor(x => x.Provider).MaximumLength(200);
            RuleFor(x => x.Notes).MaximumLength(1000);
        }
    }

    public class Handler : IRequestHandler<Command, AppointmentDto>
    {
        private readonly IAnnualHealthScreeningReminderContext _context;
        private readonly IValidator<Command> _validator;

        public Handler(IAnnualHealthScreeningReminderContext context, IValidator<Command> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<AppointmentDto> Handle(Command request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(x => x.AppointmentId == request.AppointmentId, cancellationToken)
                ?? throw new KeyNotFoundException($"Appointment with ID {request.AppointmentId} not found.");

            appointment.AppointmentDate = request.AppointmentDate;
            appointment.Location = request.Location;
            appointment.Provider = request.Provider;
            appointment.IsCompleted = request.IsCompleted;
            appointment.Notes = request.Notes;

            await _context.SaveChangesAsync(cancellationToken);

            return new AppointmentDto
            {
                AppointmentId = appointment.AppointmentId,
                UserId = appointment.UserId,
                ScreeningId = appointment.ScreeningId,
                AppointmentDate = appointment.AppointmentDate,
                Location = appointment.Location,
                Provider = appointment.Provider,
                IsCompleted = appointment.IsCompleted,
                Notes = appointment.Notes,
                CreatedAt = appointment.CreatedAt,
                IsUpcoming = appointment.IsUpcoming()
            };
        }
    }
}
