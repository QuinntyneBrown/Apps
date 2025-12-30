// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnnualHealthScreeningReminder.Api.Features.Screenings.DTOs;
using AnnualHealthScreeningReminder.Core;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AnnualHealthScreeningReminder.Api.Features.Screenings.Commands;

public class UpdateScreening
{
    public record Command(
        Guid ScreeningId,
        ScreeningType ScreeningType,
        string Name,
        int RecommendedFrequencyMonths,
        DateTime? LastScreeningDate,
        DateTime? NextDueDate,
        string? Provider,
        string? Notes) : IRequest<ScreeningDto>;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.ScreeningId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.RecommendedFrequencyMonths).GreaterThan(0).LessThanOrEqualTo(120);
            RuleFor(x => x.Provider).MaximumLength(200);
            RuleFor(x => x.Notes).MaximumLength(1000);
        }
    }

    public class Handler : IRequestHandler<Command, ScreeningDto>
    {
        private readonly IAnnualHealthScreeningReminderContext _context;
        private readonly IValidator<Command> _validator;

        public Handler(IAnnualHealthScreeningReminderContext context, IValidator<Command> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<ScreeningDto> Handle(Command request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var screening = await _context.Screenings
                .FirstOrDefaultAsync(x => x.ScreeningId == request.ScreeningId, cancellationToken)
                ?? throw new KeyNotFoundException($"Screening with ID {request.ScreeningId} not found.");

            screening.ScreeningType = request.ScreeningType;
            screening.Name = request.Name;
            screening.RecommendedFrequencyMonths = request.RecommendedFrequencyMonths;
            screening.LastScreeningDate = request.LastScreeningDate;
            screening.NextDueDate = request.NextDueDate;
            screening.Provider = request.Provider;
            screening.Notes = request.Notes;

            await _context.SaveChangesAsync(cancellationToken);

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
