using JobPortal.Application.Commands.Companies;
using JobPortal.Application.Queries.Companies;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompanyController : ControllerBase
{
    private readonly IMediator _mediator;

    public CompanyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCompanies()
    {
        var companies = await _mediator.Send(new GetAllCompaniesQuery());
        return Ok(companies);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCompanyById(Guid id)
    {
        var company = await _mediator.Send(new GetCompanyByIdQuery(id));
        if (company == null) return NotFound();
        return Ok(company);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyCommand command)
    {
        var company = await _mediator.Send(command);
        if (company.Success)
        {
            return CreatedAtAction(nameof(GetCompanyById), new { id = company.Data.Id }, company);

        }

        return BadRequest(company.ExceptionList.FirstOrDefault());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] UpdateCompanyCommand command)
    {
        if (id != command.Id) 
            return BadRequest();

        var resp =  await _mediator.Send(command);
        if (resp.Success)
        {
            return Ok(resp);
        }
        return BadRequest(resp);
    }

}
