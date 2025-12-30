// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConferenceEventManager.Core;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConferenceEventManager.Api.Features.Events;

/// <summary>
/// Command to update an existing event.
/// </summary>
public class UpdateEvent
{
    /// <summary>
    /// Command to update an event.
    /// </summary>
    public class Command : IRequest<EventDto>
    {
        public Guid EventId { get; set; }
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
    /// Validator for UpdateEvent command.
    /// </summary>
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.EventId).NotEmpty();
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
    /// Handler for UpdateEvent command.
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

            var evt = await _context.Events
                .FirstOrDefaultAsync(e => e.EventId == request.EventId, cancellationToken)
                ?? throw new KeyNotFoundException($"Event with ID {request.EventId} not found.");

            evt.UserId = request.UserId;
            evt.Name = request.Name;
            evt.EventType = request.EventType;
            evt.StartDate = request.StartDate;
            evt.EndDate = request.EndDate;
            evt.Location = request.Location;
            evt.IsVirtual = request.IsVirtual;
            evt.Website = request.Website;
            evt.RegistrationFee = request.RegistrationFee;
            evt.IsRegistered = request.IsRegistered;
            evt.DidAttend = request.DidAttend;
            evt.Notes = request.Notes;
            evt.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return EventDto.FromEvent(evt);
        }
    }
}
