using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Commands.Jobs
{
    public class CreateJobCommand : IRequest<Guid>
    {
        public Guid CompanyId { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public string? EmploymentType { get; set; }
        public decimal? Salary { get; set; }
        public string? Benefits { get; set; }
    }
}
