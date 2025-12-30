// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConferenceEventManager.Core;
using FluentValidation;
using MediatR;

namespace ConferenceEventManager.Api.Features.Sessions;

/// <summary>
/// Command to create a new session.
/// </summary>
public class CreateSession
{
    /// <summary>
    /// Command to create a session.
    /// </summary>
    public class Command : IRequest<SessionDto>
    {
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
    /// Validator for CreateSession command.
    /// </summary>
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
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
    /// Handler for CreateSession command.
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

            var session = new Session
            {
                SessionId = Guid.NewGuid(),
                UserId = request.UserId,
                EventId = request.EventId,
                Title = request.Title,
                Speaker = request.Speaker,
                Description = request.Description,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                Room = request.Room,
                PlansToAttend = request.PlansToAttend,
                DidAttend = request.DidAttend,
                Notes = request.Notes,
                CreatedAt = DateTime.UtcNow
            };

            _context.Sessions.Add(session);
            await _context.SaveChangesAsync(cancellationToken);

            return SessionDto.FromSession(session);
        }
    }
}
