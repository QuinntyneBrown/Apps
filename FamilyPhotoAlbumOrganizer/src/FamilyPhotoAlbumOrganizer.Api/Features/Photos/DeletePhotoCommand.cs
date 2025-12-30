// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyPhotoAlbumOrganizer.Core;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyPhotoAlbumOrganizer.Api.Features.Photos;

/// <summary>
/// Command to delete a photo.
/// </summary>
public class DeletePhotoCommand : IRequest<Unit>
{
    /// <summary>
    /// Gets or sets the photo ID.
    /// </summary>
    public Guid PhotoId { get; set; }
}

/// <summary>
/// Validator for DeletePhotoCommand.
/// </summary>
public class DeletePhotoCommandValidator : AbstractValidator<DeletePhotoCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeletePhotoCommandValidator"/> class.
    /// </summary>
    public DeletePhotoCommandValidator()
    {
        RuleFor(x => x.PhotoId).NotEmpty();
    }
}

/// <summary>
/// Handler for DeletePhotoCommand.
/// </summary>
public class DeletePhotoCommandHandler : IRequestHandler<DeletePhotoCommand, Unit>
{
    private readonly IFamilyPhotoAlbumOrganizerContext _context;
    private readonly IValidator<DeletePhotoCommand> _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeletePhotoCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="validator">The validator.</param>
    public DeletePhotoCommandHandler(
        IFamilyPhotoAlbumOrganizerContext context,
        IValidator<DeletePhotoCommand> validator)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    /// <summary>
    /// Handles the DeletePhotoCommand.
    /// </summary>
    /// <param name="request">The command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Unit.</returns>
    public async Task<Unit> Handle(DeletePhotoCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var photo = await _context.Photos
            .FirstOrDefaultAsync(p => p.PhotoId == request.PhotoId, cancellationToken);

        if (photo == null)
        {
            throw new KeyNotFoundException($"Photo with ID {request.PhotoId} not found.");
        }

        _context.Photos.Remove(photo);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
