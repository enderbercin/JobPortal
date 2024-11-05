using JobPortal.Application.Queries.Companies;
using JobPortal.Domain.Abstructs;
using JobPortal.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Handlers.Companies
{
    public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, Company>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCompanyByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Company> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Companies.GetByIdAsync(request.Id);
        }
    }
}
