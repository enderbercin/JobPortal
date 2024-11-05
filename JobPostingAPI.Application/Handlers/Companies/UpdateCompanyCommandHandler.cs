using JobPortal.Application.Commands.Companies;
using JobPortal.Domain.Abstructs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Handlers.Companies
{
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCompanyCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _unitOfWork.Companies.GetByIdAsync(request.Id);

            if (company == null)
                throw new KeyNotFoundException("Şirket bulunamadı.");

            company.Name = request.Name;
            company.PhoneNumber = request.PhoneNumber;
            company.Address = request.Address;

            _unitOfWork.Companies.Update(company);
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}
