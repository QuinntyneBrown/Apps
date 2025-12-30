// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConferenceEventManager.Core;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConferenceEventManager.Api.Features.Sessions;

/// <summary>
/// Command to update an existing session.
/// </summary>
public class UpdateSession
{
    /// <summary>
    /// Command to update a session.
    /// </summary>
    public class Command : IRequest<SessionDto>
    {
        public Guid SessionId { get; set; }
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Speaker { get; set; }
        public string? Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Room { get; set; }
        public bool PlansToAttend { get; set; }
        public bool DidAttend { get; set; }
        public string? Notes { get; set; }
    }

    /// <summary>
    /// Validator for UpdateSession command.
    /// </summary>
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.SessionId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.EventId).NotEmpty();
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Speaker).MaximumLength(200).When(x => !string.IsNullOrEmpty(x.Speaker));
            RuleFor(x => x.Description).MaximumLength(2000).When(x => !string.IsNullOrEmpty(x.Description));
            RuleFor(x => x.StartTime).NotEmpty();
            RuleFor(x => x.EndTime).GreaterThanOrEqualTo(x => x.StartTime).When(x => x.EndTime.HasValue);
            RuleFor(x => x.Room).MaximumLength(100).When(x => !string.IsNullOrEmpty(x.Room));
        }
    }

    /// <summary>
    /// Handler for UpdateSession command.
    /// </summary>
    public class Handler : IRequestHandler<Command, SessionDto>
    {
        private readonly IConferenceEventManagerContext _context;
        private readonly IValidator<Command> _validator;

        public Handler(IConferenceEventManagerContext context, IValidator<Command> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<SessionDto> Handle(Command request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var session = await _context.Sessions
                .FirstOrDefaultAsync(s => s.SessionId == request.SessionId, cancellationToken)
                ?? throw new KeyNotFoundException($"Session with ID {request.SessionId} not found.");

            session.UserId = request.UserId;
            session.EventId = request.EventId;
            session.Title = request.Title;
            session.Speaker = request.Speaker;
            session.Description = request.Description;
            session.StartTime = request.StartTime;
            session.EndTime = request.EndTime;
            session.Room = request.Room;
            session.PlansToAttend = request.PlansToAttend;
            session.DidAttend = request.DidAttend;
            session.Notes = request.Notes;
            session.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return SessionDto.FromSession(session);
        }
    }
}
