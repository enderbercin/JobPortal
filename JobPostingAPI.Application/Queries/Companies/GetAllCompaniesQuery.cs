using JobPortal.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Queries.Companies
{
    public class GetAllCompaniesQuery : IRequest<IEnumerable<Company>> { }
}
