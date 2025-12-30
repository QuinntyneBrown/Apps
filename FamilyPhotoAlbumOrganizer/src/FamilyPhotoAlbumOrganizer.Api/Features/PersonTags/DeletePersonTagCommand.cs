// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyPhotoAlbumOrganizer.Core;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyPhotoAlbumOrganizer.Api.Features.PersonTags;

/// <summary>
/// Command to delete a person tag.
/// </summary>
public class DeletePersonTagCommand : IRequest<Unit>
{
    /// <summary>
    /// Gets or sets the person tag ID.
    /// </summary>
    public Guid PersonTagId { get; set; }
}

/// <summary>
/// Validator for DeletePersonTagCommand.
/// </summary>
public class DeletePersonTagCommandValidator : AbstractValidator<DeletePersonTagCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeletePersonTagCommandValidator"/> class.
    /// </summary>
    public DeletePersonTagCommandValidator()
    {
        RuleFor(x => x.PersonTagId).NotEmpty();
    }
}

/// <summary>
/// Handler for DeletePersonTagCommand.
/// </summary>
public class DeletePersonTagCommandHandler : IRequestHandler<DeletePersonTagCommand, Unit>
{
    private readonly IFamilyPhotoAlbumOrganizerContext _context;
    private readonly IValidator<DeletePersonTagCommand> _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeletePersonTagCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="validator">The validator.</param>
    public DeletePersonTagCommandHandler(
        IFamilyPhotoAlbumOrganizerContext context,
        IValidator<DeletePersonTagCommand> validator)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    /// <summary>
    /// Handles the DeletePersonTagCommand.
    /// </summary>
    /// <param name="request">The command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Unit.</returns>
    public async Task<Unit> Handle(DeletePersonTagCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var personTag = await _context.PersonTags
            .FirstOrDefaultAsync(pt => pt.PersonTagId == request.PersonTagId, cancellationToken);

        if (personTag == null)
        {
            throw new KeyNotFoundException($"PersonTag with ID {request.PersonTagId} not found.");
        }

        _context.PersonTags.Remove(personTag);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
