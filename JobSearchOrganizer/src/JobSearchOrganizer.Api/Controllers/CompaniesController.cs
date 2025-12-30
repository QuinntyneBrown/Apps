using JobSearchOrganizer.Api.Features.Companies;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JobSearchOrganizer.Api.Controllers;

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
    public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompanies(
        [FromQuery] Guid? userId,
        [FromQuery] string? industry,
        [FromQuery] bool? isTargetCompany)
    {
        _logger.LogInformation("Getting companies for user {UserId}", userId);

        var result = await _mediator.Send(new GetCompaniesQuery
        {
            UserId = userId,
            Industry = industry,
            IsTargetCompany = isTargetCompany,
        });

        return Ok(result);
    }

    [HttpGet("{companyId:guid}")]
    [ProducesResponseType(typeof(CompanyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CompanyDto>> GetCompanyById(Guid companyId)
    {
        _logger.LogInformation("Getting company {CompanyId}", companyId);

        var result = await _mediator.Send(new GetCompanyByIdQuery { CompanyId = companyId });

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CompanyDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CompanyDto>> CreateCompany([FromBody] CreateCompanyCommand command)
    {
        _logger.LogInformation("Creating company for user {UserId}", command.UserId);

        var result = await _mediator.Send(command);

        return Created($"/api/companies/{result.CompanyId}", result);
    }

    [HttpPut("{companyId:guid}")]
    [ProducesResponseType(typeof(CompanyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CompanyDto>> UpdateCompany(Guid companyId, [FromBody] UpdateCompanyCommand command)
    {
        if (companyId != command.CompanyId)
        {
            return BadRequest("Company ID mismatch");
        }

        _logger.LogInformation("Updating company {CompanyId}", companyId);

        var result = await _mediator.Send(command);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{companyId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCompany(Guid companyId)
    {
        _logger.LogInformation("Deleting company {CompanyId}", companyId);

        var result = await _mediator.Send(new DeleteCompanyCommand { CompanyId = companyId });

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
