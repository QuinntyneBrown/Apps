// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConferenceEventManager.Core;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConferenceEventManager.Api.Features.Events;

/// <summary>
/// Command to create a new event.
/// </summary>
public class CreateEvent
{
    /// <summary>
    /// Command to create an event.
    /// </summary>
    public class Command : IRequest<EventDto>
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public EventType EventType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Location { get; set; }
        public bool IsVirtual { get; set; }
        public string? Website { get; set; }
        public decimal? RegistrationFee { get; set; }
        public bool IsRegistered { get; set; }
        public bool DidAttend { get; set; }
        public string? Notes { get; set; }
    }

    /// <summary>
    /// Validator for CreateEvent command.
    /// </summary>
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.EventType).IsInEnum();
            RuleFor(x => x.StartDate).NotEmpty();
            RuleFor(x => x.EndDate).NotEmpty().GreaterThanOrEqualTo(x => x.StartDate);
            RuleFor(x => x.Location).MaximumLength(500).When(x => !string.IsNullOrEmpty(x.Location));
            RuleFor(x => x.Website).MaximumLength(500).When(x => !string.IsNullOrEmpty(x.Website));
            RuleFor(x => x.RegistrationFee).GreaterThanOrEqualTo(0).When(x => x.RegistrationFee.HasValue);
        }
    }

    /// <summary>
    /// Handler for CreateEvent command.
    /// </summary>
    public class Handler : IRequestHandler<Command, EventDto>
    {
        private readonly IConferenceEventManagerContext _context;
        private readonly IValidator<Command> _validator;

        public Handler(IConferenceEventManagerContext context, IValidator<Command> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<EventDto> Handle(Command request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var evt = new Event
            {
                EventId = Guid.NewGuid(),
                UserId = request.UserId,
                Name = request.Name,
                EventType = request.EventType,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Location = request.Location,
                IsVirtual = request.IsVirtual,
                Website = request.Website,
                RegistrationFee = request.RegistrationFee,
                IsRegistered = request.IsRegistered,
                DidAttend = request.DidAttend,
                Notes = request.Notes,
                CreatedAt = DateTime.UtcNow
            };

            _context.Events.Add(evt);
            await _context.SaveChangesAsync(cancellationToken);

            return EventDto.FromEvent(evt);
        }
    }
}
