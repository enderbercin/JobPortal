using JobPortal.Domain.Entities;
using JobPortal.Infrastructure.Repository.Abstracts;
using MediatR;

namespace JobPortal.Application.Commands.Companies;

public class UpdateCompanyCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
}
public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCompanyCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        var repo = _unitOfWork.GetRepository<Company>();
        var company = repo.GetById(request.Id);
        if (company == null)
            throw new KeyNotFoundException("Şirket bulunamadı.");

        company.Name = request.Name;
        company.PhoneNumber = request.PhoneNumber;
        company.Address = request.Address;

        await repo.UpdateAsync(company);
        if (await _unitOfWork.SaveChangesAsync() > 0)

            return true;

        return false;
    }


}
