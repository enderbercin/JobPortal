using JobPortal.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Queries.Companies
{
    public class GetCompanyByIdQuery : IRequest<Company>
    {
        public Guid Id { get; set; }

        public GetCompanyByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
