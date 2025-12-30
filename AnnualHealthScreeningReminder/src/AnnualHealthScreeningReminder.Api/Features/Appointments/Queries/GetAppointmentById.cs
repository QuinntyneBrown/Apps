// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnnualHealthScreeningReminder.Api.Features.Appointments.DTOs;
using AnnualHealthScreeningReminder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AnnualHealthScreeningReminder.Api.Features.Appointments.Queries;

public class GetAppointmentById
{
    public record Query(Guid AppointmentId) : IRequest<AppointmentDto>;

    public class Handler : IRequestHandler<Query, AppointmentDto>
    {
        private readonly IAnnualHealthScreeningReminderContext _context;

        public Handler(IAnnualHealthScreeningReminderContext context)
        {
            _context = context;
        }

        public async Task<AppointmentDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var appointment = await _context.Appointments
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.AppointmentId == request.AppointmentId, cancellationToken)
                ?? throw new KeyNotFoundException($"Appointment with ID {request.AppointmentId} not found.");

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
