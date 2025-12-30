// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnnualHealthScreeningReminder.Api.Features.Screenings.DTOs;
using AnnualHealthScreeningReminder.Core;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AnnualHealthScreeningReminder.Api.Features.Screenings.Commands;

public class CreateScreening
{
    public record Command(
        Guid UserId,
        ScreeningType ScreeningType,
        string Name,
        int RecommendedFrequencyMonths,
        DateTime? LastScreeningDate,
        string? Provider,
        string? Notes) : IRequest<ScreeningDto>;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.UserId).NotEmpty();
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

            var screening = new Screening
            {
                ScreeningId = Guid.NewGuid(),
                UserId = request.UserId,
                ScreeningType = request.ScreeningType,
                Name = request.Name,
                RecommendedFrequencyMonths = request.RecommendedFrequencyMonths,
                LastScreeningDate = request.LastScreeningDate,
                NextDueDate = request.LastScreeningDate?.AddMonths(request.RecommendedFrequencyMonths),
                Provider = request.Provider,
                Notes = request.Notes,
                CreatedAt = DateTime.UtcNow
            };

            _context.Screenings.Add(screening);
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
