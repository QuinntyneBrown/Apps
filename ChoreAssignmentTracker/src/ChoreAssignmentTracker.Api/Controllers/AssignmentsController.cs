// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ChoreAssignmentTracker.Api.Features.Assignments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChoreAssignmentTracker.Api.Controllers;

/// <summary>
/// Controller for managing assignments.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AssignmentsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AssignmentsController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AssignmentsController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="logger">The logger.</param>
    public AssignmentsController(IMediator mediator, ILogger<AssignmentsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Gets all assignments.
    /// </summary>
    /// <param name="familyMemberId">Optional family member ID to filter by.</param>
    /// <param name="choreId">Optional chore ID to filter by.</param>
    /// <param name="isCompleted">Optional completed status to filter by.</param>
    /// <param name="isOverdue">Optional overdue status to filter by.</param>
    /// <returns>A list of assignments.</returns>
    [HttpGet]
    public async Task<ActionResult<List<AssignmentDto>>> GetAssignments(
        [FromQuery] Guid? familyMemberId,
        [FromQuery] Guid? choreId,
        [FromQuery] bool? isCompleted,
        [FromQuery] bool? isOverdue)
    {
        _logger.LogInformation("Getting assignments");

        var result = await _mediator.Send(new GetAssignments
        {
            FamilyMemberId = familyMemberId,
            ChoreId = choreId,
            IsCompleted = isCompleted,
            IsOverdue = isOverdue
        });

        return Ok(result);
    }

    /// <summary>
    /// Gets an assignment by ID.
    /// </summary>
    /// <param name="id">The assignment ID.</param>
    /// <returns>The assignment.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<AssignmentDto>> GetAssignment(Guid id)
    {
        _logger.LogInformation("Getting assignment {AssignmentId}", id);

        var result = await _mediator.Send(new GetAssignmentById { AssignmentId = id });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Creates a new assignment.
    /// </summary>
    /// <param name="dto">The assignment data.</param>
    /// <returns>The created assignment.</returns>
    [HttpPost]
    public async Task<ActionResult<AssignmentDto>> CreateAssignment(CreateAssignmentDto dto)
    {
        _logger.LogInformation("Creating assignment for chore {ChoreId} and family member {FamilyMemberId}", dto.ChoreId, dto.FamilyMemberId);

        var result = await _mediator.Send(new CreateAssignment { Assignment = dto });

        if (result == null)
        {
            return BadRequest("Unable to create assignment. Check if chore and family member exist.");
        }

        return CreatedAtAction(nameof(GetAssignment), new { id = result.AssignmentId }, result);
    }

    /// <summary>
    /// Updates an assignment.
    /// </summary>
    /// <param name="id">The assignment ID.</param>
    /// <param name="dto">The assignment data.</param>
    /// <returns>The updated assignment.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<AssignmentDto>> UpdateAssignment(Guid id, UpdateAssignmentDto dto)
    {
        _logger.LogInformation("Updating assignment {AssignmentId}", id);

        var result = await _mediator.Send(new UpdateAssignment { AssignmentId = id, Assignment = dto });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Deletes an assignment.
    /// </summary>
    /// <param name="id">The assignment ID.</param>
    /// <returns>No content if successful.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAssignment(Guid id)
    {
        _logger.LogInformation("Deleting assignment {AssignmentId}", id);

        var result = await _mediator.Send(new DeleteAssignment { AssignmentId = id });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Marks an assignment as completed.
    /// </summary>
    /// <param name="id">The assignment ID.</param>
    /// <param name="dto">The completion data.</param>
    /// <returns>The completed assignment.</returns>
    [HttpPost("{id}/complete")]
    public async Task<ActionResult<AssignmentDto>> CompleteAssignment(Guid id, CompleteAssignmentDto dto)
    {
        _logger.LogInformation("Completing assignment {AssignmentId}", id);

        var result = await _mediator.Send(new CompleteAssignment { AssignmentId = id, CompletionData = dto });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Verifies a completed assignment and awards points.
    /// </summary>
    /// <param name="id">The assignment ID.</param>
    /// <param name="dto">The verification data.</param>
    /// <returns>The verified assignment.</returns>
    [HttpPost("{id}/verify")]
    public async Task<ActionResult<AssignmentDto>> VerifyAssignment(Guid id, VerifyAssignmentDto dto)
    {
        _logger.LogInformation("Verifying assignment {AssignmentId}", id);

        var result = await _mediator.Send(new VerifyAssignment { AssignmentId = id, VerificationData = dto });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}
