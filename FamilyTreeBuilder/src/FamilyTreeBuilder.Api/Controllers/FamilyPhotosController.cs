// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyTreeBuilder.Api.Features.FamilyPhotos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTreeBuilder.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FamilyPhotosController : ControllerBase
{
    private readonly IMediator _mediator;

    public FamilyPhotosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<FamilyPhotoDto>>> GetFamilyPhotos([FromQuery] Guid? personId)
    {
        var query = new GetFamilyPhotos.Query { PersonId = personId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FamilyPhotoDto>> GetFamilyPhotoById(Guid id)
    {
        var query = new GetFamilyPhotoById.Query { FamilyPhotoId = id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<FamilyPhotoDto>> CreateFamilyPhoto([FromBody] CreateFamilyPhoto.Command command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetFamilyPhotoById), new { id = result.FamilyPhotoId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<FamilyPhotoDto>> UpdateFamilyPhoto(Guid id, [FromBody] UpdateFamilyPhoto.Command command)
    {
        command.FamilyPhotoId = id;
        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteFamilyPhoto(Guid id)
    {
        var command = new DeleteFamilyPhoto.Command { FamilyPhotoId = id };
        var result = await _mediator.Send(command);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
