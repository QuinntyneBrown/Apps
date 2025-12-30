// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnnualHealthScreeningReminder.Api.Features.Screenings.DTOs;
using AnnualHealthScreeningReminder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AnnualHealthScreeningReminder.Api.Features.Screenings.Queries;

public class GetScreeningById
{
    public record Query(Guid ScreeningId) : IRequest<ScreeningDto>;

    public class Handler : IRequestHandler<Query, ScreeningDto>
    {
        private readonly IAnnualHealthScreeningReminderContext _context;

        public Handler(IAnnualHealthScreeningReminderContext context)
        {
            _context = context;
        }

        public async Task<ScreeningDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var screening = await _context.Screenings
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ScreeningId == request.ScreeningId, cancellationToken)
                ?? throw new KeyNotFoundException($"Screening with ID {request.ScreeningId} not found.");

            return new ScreeningDto
            {
                ScreeningId = screening.ScreeningId,
                UserId = screening.UserId,
                ScreeningType = screening.ScreeningType,
                Name = screening.Name,
                RecommendedFrequencyMonths = screening.RecommendedFrequencyMonths,
                LastScreeningDate = screening.LastScreeningDate,
                NextDueDate = screening.NextDueDate,
                Provider = screening.Provider,
                Notes = screening.Notes,
                CreatedAt = screening.CreatedAt,
                IsDueSoon = screening.IsDueSoon()
            };
        }
    }
}
