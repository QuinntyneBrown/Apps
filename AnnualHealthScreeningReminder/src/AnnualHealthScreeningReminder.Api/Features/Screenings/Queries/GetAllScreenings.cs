// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnnualHealthScreeningReminder.Api.Features.Screenings.DTOs;
using AnnualHealthScreeningReminder.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AnnualHealthScreeningReminder.Api.Features.Screenings.Queries;

public class GetAllScreenings
{
    public record Query(Guid? UserId = null) : IRequest<List<ScreeningDto>>;

    public class Handler : IRequestHandler<Query, List<ScreeningDto>>
    {
        private readonly IAnnualHealthScreeningReminderContext _context;

        public Handler(IAnnualHealthScreeningReminderContext context)
        {
            _context = context;
        }

        public async Task<List<ScreeningDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _context.Screenings.AsNoTracking();

            if (request.UserId.HasValue)
            {
                query = query.Where(x => x.UserId == request.UserId.Value);
            }

            var screenings = await query
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync(cancellationToken);

            return screenings.Select(screening => new ScreeningDto
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
            }).ToList();
        }
    }
}
