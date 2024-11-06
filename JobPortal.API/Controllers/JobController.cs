using JobPortal.Application.Commands.Jobs;
using JobPortal.Application.Queries.Jobs;
using JobPortal.Domain;
using JobPortal.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JobController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseServiceResponse<Job>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseServiceResponse))]
        public async Task<IActionResult> CreateJob([FromBody] CreateJobCommand command)
        {
            try
            {

                var job = await _mediator.Send(command);
                if (!job.Success)
                {
                    return BadRequest(job.ExceptionList.FirstOrDefault());
                }
                return Ok(job);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchJobsByExpirationDate([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var query = new GetJobsByExpirationDateQuery(startDate, endDate);
            var jobs = await _mediator.Send(query);
            return Ok(jobs);
        }
    }
}
