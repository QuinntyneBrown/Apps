// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConferenceEventManager.Core;
using FluentValidation;
using MediatR;

namespace ConferenceEventManager.Api.Features.Notes;

/// <summary>
/// Command to create a new note.
/// </summary>
public class CreateNote
{
    /// <summary>
    /// Command to create a note.
    /// </summary>
    public class Command : IRequest<NoteDto>
    {
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
        public string Content { get; set; } = string.Empty;
        public string? Category { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
    }

    /// <summary>
    /// Validator for CreateNote command.
    /// </summary>
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.EventId).NotEmpty();
            RuleFor(x => x.Content).NotEmpty().MaximumLength(5000);
            RuleFor(x => x.Category).MaximumLength(100).When(x => !string.IsNullOrEmpty(x.Category));
        }
    }

    /// <summary>
    /// Handler for CreateNote command.
    /// </summary>
    public class Handler : IRequestHandler<Command, NoteDto>
    {
        private readonly IConferenceEventManagerContext _context;
        private readonly IValidator<Command> _validator;

        public Handler(IConferenceEventManagerContext context, IValidator<Command> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<NoteDto> Handle(Command request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var note = new Note
            {
                NoteId = Guid.NewGuid(),
                UserId = request.UserId,
                EventId = request.EventId,
                Content = request.Content,
                Category = request.Category,
                Tags = request.Tags ?? new List<string>(),
                CreatedAt = DateTime.UtcNow
            };

            _context.Notes.Add(note);
            await _context.SaveChangesAsync(cancellationToken);

            return NoteDto.FromNote(note);
        }
    }
}
