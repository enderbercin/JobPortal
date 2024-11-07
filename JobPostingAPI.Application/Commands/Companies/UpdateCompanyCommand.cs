using JobPortal.Application.Services.Abstracts;
using JobPortal.Domain;
using JobPortal.Domain.Entities;
using JobPortal.Infrastructure.Repository.Abstracts;
using MediatR;

namespace JobPortal.Application.Commands.Companies;

public class UpdateCompanyCommand : IRequest<BaseServiceResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public int PostingQuota { get; set; }
}
public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, BaseServiceResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICompanySyncService _companySync;

    public UpdateCompanyCommandHandler(IUnitOfWork unitOfWork, ICompanySyncService companySync)
    {
        _unitOfWork = unitOfWork;
        _companySync = companySync;
    }

    public async Task<BaseServiceResponse> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        var resp = new BaseServiceResponse();
        var repo = _unitOfWork.GetRepository<Company>();
        var company = repo.GetById(request.Id);
        if (company == null)
        {
            resp.Status = 400;
            resp.Success = false;
            resp.ExceptionList.Add("Şirket bulunamadı.");
        }
        var isExistComp = await _unitOfWork.CompanyRepository.GetByPhoneNumber(request.PhoneNumber);
        if (isExistComp != null)
        {
            resp.ExceptionList.Add("Bu telefon numarası ile daha önce kayıt yapılmış : " + request.PhoneNumber);
            resp.Success = false;
            resp.Status = 400;
            return resp;
        }
        company.Name = request.Name;
        company.PhoneNumber = request.PhoneNumber;
        company.Address = request.Address;
        company.PostingQuota = request.PostingQuota;

        await repo.UpdateAsync(company);
        if (await _unitOfWork.SaveChangesAsync() > 0)
        {
            resp.Status = 200;
            resp.Success = true;
            return resp;
        }
        resp.Status = 400;
        resp.Success = false;
        return resp;
    }


}
