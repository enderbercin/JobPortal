using JobPortal.Application.Commands.Companies;
using JobPortal.Domain.Abstructs;
using JobPortal.Domain.Entities;
using JobPortal.Infrastructure.Repository.Abstructs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Handlers.Companies
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, Company>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCompanyCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Company> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = new Company
            {
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address
            };

            await _unitOfWork.Companies.AddAsync(company);
            await _unitOfWork.CompleteAsync();

            return company;
        }
    }
}