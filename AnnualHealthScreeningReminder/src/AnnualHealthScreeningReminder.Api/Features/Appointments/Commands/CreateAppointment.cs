// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnnualHealthScreeningReminder.Api.Features.Appointments.DTOs;
using AnnualHealthScreeningReminder.Core;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AnnualHealthScreeningReminder.Api.Features.Appointments.Commands;

public class CreateAppointment
{
    public record Command(
        Guid UserId,
        Guid ScreeningId,
        DateTime AppointmentDate,
        string Location,
        string? Provider,
        string? Notes) : IRequest<AppointmentDto>;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.ScreeningId).NotEmpty();
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

            var appointment = new Appointment
            {
                AppointmentId = Guid.NewGuid(),
                UserId = request.UserId,
                ScreeningId = request.ScreeningId,
                AppointmentDate = request.AppointmentDate,
                Location = request.Location,
                Provider = request.Provider,
                IsCompleted = false,
                Notes = request.Notes,
                CreatedAt = DateTime.UtcNow
            };

            _context.Appointments.Add(appointment);
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
