// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyPhotoAlbumOrganizer.Core;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyPhotoAlbumOrganizer.Api.Features.Albums;

/// <summary>
/// Command to delete an album.
/// </summary>
public class DeleteAlbumCommand : IRequest<Unit>
{
    /// <summary>
    /// Gets or sets the album ID.
    /// </summary>
    public Guid AlbumId { get; set; }
}

/// <summary>
/// Validator for DeleteAlbumCommand.
/// </summary>
public class DeleteAlbumCommandValidator : AbstractValidator<DeleteAlbumCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteAlbumCommandValidator"/> class.
    /// </summary>
    public DeleteAlbumCommandValidator()
    {
        RuleFor(x => x.AlbumId).NotEmpty();
    }
}

/// <summary>
/// Handler for DeleteAlbumCommand.
/// </summary>
public class DeleteAlbumCommandHandler : IRequestHandler<DeleteAlbumCommand, Unit>
{
    private readonly IFamilyPhotoAlbumOrganizerContext _context;
    private readonly IValidator<DeleteAlbumCommand> _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteAlbumCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="validator">The validator.</param>
    public DeleteAlbumCommandHandler(
        IFamilyPhotoAlbumOrganizerContext context,
        IValidator<DeleteAlbumCommand> validator)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    /// <summary>
    /// Handles the DeleteAlbumCommand.
    /// </summary>
    /// <param name="request">The command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>Unit.</returns>
    public async Task<Unit> Handle(DeleteAlbumCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var album = await _context.Albums
            .FirstOrDefaultAsync(a => a.AlbumId == request.AlbumId, cancellationToken);

        if (album == null)
        {
            throw new KeyNotFoundException($"Album with ID {request.AlbumId} not found.");
        }

        _context.Albums.Remove(album);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
