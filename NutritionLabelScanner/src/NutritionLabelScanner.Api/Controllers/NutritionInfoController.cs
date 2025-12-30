// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using NutritionLabelScanner.Api.Features.NutritionInfo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NutritionLabelScanner.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NutritionInfoController : ControllerBase
{
    private readonly IMediator _mediator;

    public NutritionInfoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<NutritionInfoDto>>> GetNutritionInfos(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetNutritionInfosQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<NutritionInfoDto>> GetNutritionInfoById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new GetNutritionInfoByIdQuery(id), cancellationToken);
            return Ok(result);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult<NutritionInfoDto>> CreateNutritionInfo(
        [FromBody] CreateNutritionInfoCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetNutritionInfoById), new { id = result.NutritionInfoId }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<NutritionInfoDto>> UpdateNutritionInfo(
        Guid id,
        [FromBody] UpdateNutritionInfoCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.NutritionInfoId)
        {
            return BadRequest("ID mismatch");
        }

        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNutritionInfo(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _mediator.Send(new DeleteNutritionInfoCommand(id), cancellationToken);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
}
