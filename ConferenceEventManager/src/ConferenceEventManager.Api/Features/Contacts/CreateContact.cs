// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConferenceEventManager.Core;
using FluentValidation;
using MediatR;

namespace ConferenceEventManager.Api.Features.Contacts;

/// <summary>
/// Command to create a new contact.
/// </summary>
public class CreateContact
{
    /// <summary>
    /// Command to create a contact.
    /// </summary>
    public class Command : IRequest<ContactDto>
    {
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Company { get; set; }
        public string? JobTitle { get; set; }
        public string? Email { get; set; }
        public string? LinkedInUrl { get; set; }
        public string? Notes { get; set; }
    }

    /// <summary>
    /// Validator for CreateContact command.
    /// </summary>
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.EventId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Company).MaximumLength(200).When(x => !string.IsNullOrEmpty(x.Company));
            RuleFor(x => x.JobTitle).MaximumLength(200).When(x => !string.IsNullOrEmpty(x.JobTitle));
            RuleFor(x => x.Email).EmailAddress().MaximumLength(200).When(x => !string.IsNullOrEmpty(x.Email));
            RuleFor(x => x.LinkedInUrl).MaximumLength(500).When(x => !string.IsNullOrEmpty(x.LinkedInUrl));
        }
    }

    /// <summary>
    /// Handler for CreateContact command.
    /// </summary>
    public class Handler : IRequestHandler<Command, ContactDto>
    {
        private readonly IConferenceEventManagerContext _context;
        private readonly IValidator<Command> _validator;

        public Handler(IConferenceEventManagerContext context, IValidator<Command> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<ContactDto> Handle(Command request, CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);

            var contact = new Contact
            {
                ContactId = Guid.NewGuid(),
                UserId = request.UserId,
                EventId = request.EventId,
                Name = request.Name,
                Company = request.Company,
                JobTitle = request.JobTitle,
                Email = request.Email,
                LinkedInUrl = request.LinkedInUrl,
                Notes = request.Notes,
                CreatedAt = DateTime.UtcNow
            };

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync(cancellationToken);

            return ContactDto.FromContact(contact);
        }
    }
}
