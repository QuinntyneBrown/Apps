// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnnualHealthScreeningReminder.Core;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AnnualHealthScreeningReminder.Api.Features.Screenings.Commands;

public class DeleteScreening
{
    public record Command(Guid ScreeningId) : IRequest<Unit>;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.ScreeningId).NotEmpty();
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

            var screening = await _context.Screenings
                .FirstOrDefaultAsync(x => x.ScreeningId == request.ScreeningId, cancellationToken)
                ?? throw new KeyNotFoundException($"Screening with ID {request.ScreeningId} not found.");

            _context.Screenings.Remove(screening);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
