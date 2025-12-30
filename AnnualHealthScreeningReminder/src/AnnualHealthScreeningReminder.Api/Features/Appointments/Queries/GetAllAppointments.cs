// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnnualHealthScreeningReminder.Api.Features.Appointments.DTOs;
using AnnualHealthScreeningReminder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AnnualHealthScreeningReminder.Api.Features.Appointments.Queries;

public class GetAllAppointments
{
    public record Query(Guid? UserId = null, Guid? ScreeningId = null) : IRequest<List<AppointmentDto>>;

    public class Handler : IRequestHandler<Query, List<AppointmentDto>>
    {
        private readonly IAnnualHealthScreeningReminderContext _context;

        public Handler(IAnnualHealthScreeningReminderContext context)
        {
            _context = context;
        }

        public async Task<List<AppointmentDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _context.Appointments.AsNoTracking();

            if (request.UserId.HasValue)
            {
                query = query.Where(x => x.UserId == request.UserId.Value);
            }

            if (request.ScreeningId.HasValue)
            {
                query = query.Where(x => x.ScreeningId == request.ScreeningId.Value);
            }

            var appointments = await query
                .OrderByDescending(x => x.AppointmentDate)
                .ToListAsync(cancellationToken);

            return appointments.Select(appointment => new AppointmentDto
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
            }).ToList();
        }
    }
}
