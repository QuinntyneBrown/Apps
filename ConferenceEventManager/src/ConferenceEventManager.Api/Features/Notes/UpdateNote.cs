// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConferenceEventManager.Core;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConferenceEventManager.Api.Features.Notes;

/// <summary>
/// Command to update an existing note.
/// </summary>
public class UpdateNote
{
    /// <summary>
    /// Command to update a note.
    /// </summary>
    public class Command : IRequest<NoteDto>
    {
        public Guid NoteId { get; set; }
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
        public string Content { get; set; } = string.Empty;
        public string? Category { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
    }

    /// <summary>
    /// Validator for UpdateNote command.
    /// </summary>
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.NoteId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.EventId).NotEmpty();
            RuleFor(x => x.Content).NotEmpty().MaximumLength(5000);
            RuleFor(x => x.Category).MaximumLength(100).When(x => !string.IsNullOrEmpty(x.Category));
        }
    }

    /// <summary>
    /// Handler for UpdateNote command.
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

            var note = await _context.Notes
                .FirstOrDefaultAsync(n => n.NoteId == request.NoteId, cancellationToken)
                ?? throw new KeyNotFoundException($"Note with ID {request.NoteId} not found.");

            note.UserId = request.UserId;
            note.EventId = request.EventId;
            note.Content = request.Content;
            note.Category = request.Category;
            note.Tags = request.Tags ?? new List<string>();
            note.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return NoteDto.FromNote(note);
        }
    }
}
