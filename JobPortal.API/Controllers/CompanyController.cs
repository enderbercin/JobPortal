using JobPortal.Application.Commands.Companies;
using JobPortal.Application.Queries.Companies;
using JobPortal.Application.Services.Abstructs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICompanySyncService _companySync;

        public CompanyController(IMediator mediator, ICompanySyncService companySync)
        {
            _mediator = mediator;
            _companySync = companySync;
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
                _companySync.AddOrUpdateCompanyToElastic(company.Data.Id);
                return CreatedAtAction(nameof(GetCompanyById), new { id = company.Data.Id }, company);

            }

            return BadRequest(company.ExceptionList.FirstOrDefault());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] UpdateCompanyCommand command)
        {
            if (id != command.Id) return BadRequest();
            await _mediator.Send(command);
            return NoContent();
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCompany(int id)
        //{
        //    await _mediator.Send(new DeleteCompanyCommand(id));
        //    return NoContent();
        //}
    }
}
