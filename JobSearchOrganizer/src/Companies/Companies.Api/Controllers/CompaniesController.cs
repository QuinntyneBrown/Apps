using Companies.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Companies.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompaniesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CompaniesController> _logger;

    public CompaniesController(IMediator mediator, ILogger<CompaniesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CompanyDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompanies(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting all companies");
        var result = await _mediator.Send(new GetCompaniesQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CompanyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CompanyDto>> GetCompanyById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting company {CompanyId}", id);
        var result = await _mediator.Send(new GetCompanyByIdQuery(id), cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CompanyDto), StatusCodes.Status201Created)]
    public async Task<ActionResult<CompanyDto>> CreateCompany(
        [FromBody] CreateCompanyCommand command,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating company {Name}", command.Name);
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetCompanyById), new { id = result.CompanyId }, result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(CompanyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CompanyDto>> UpdateCompany(
        Guid id,
        [FromBody] UpdateCompanyCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.CompanyId) return BadRequest("ID mismatch");
        _logger.LogInformation("Updating company {CompanyId}", id);
        var result = await _mediator.Send(command, cancellationToken);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCompany(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting company {CompanyId}", id);
        var result = await _mediator.Send(new DeleteCompanyCommand(id), cancellationToken);
        if (!result) return NotFound();
        return NoContent();
    }
}
