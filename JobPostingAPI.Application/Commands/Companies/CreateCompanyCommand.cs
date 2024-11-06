using JobPortal.Application.Services.Abstracts;
using JobPortal.Domain;
using JobPortal.Domain.Entities;
using JobPortal.Infrastructure.Repository.Abstracts;
using MediatR;

namespace JobPortal.Application.Commands.Companies;

public class CreateCompanyCommand : IRequest<BaseServiceResponse<Company>>
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
}
public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, BaseServiceResponse<Company>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICompanySyncService _companySync;

    public CreateCompanyCommandHandler(IUnitOfWork unitOfWork, ICompanySyncService companySync)
    {
        _unitOfWork = unitOfWork;
        _companySync = companySync;
    }

    public async Task<BaseServiceResponse<Company>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var isExistComp = await _unitOfWork.CompanyRepository.GetByPhoneNumber(request.PhoneNumber);
            var resp = new BaseServiceResponse<Company>();
            if (isExistComp != null)
            {
                resp.Data = isExistComp;
                resp.ExceptionList.Add("Bu telefon numarası ile daha önce kayıt yapılmış : " + request.PhoneNumber);
                resp.Success = false;
                resp.Status = 400;
                return resp;
            }
            var company = new Company
            {
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address
            };

            await _unitOfWork.GetRepository<Company>().AddAsync(company);
            if (await _unitOfWork.SaveChangesAsync() > 0)
            {
                resp.Data = company;
                resp.Status = 200;
                resp.Success = true;
                _companySync.AddOrUpdateCompanyToElastic(company.Id);

                return resp;
            };
            resp.Success = false;
            resp.Status = 400;
            return resp;
        }
        catch (Exception ex)
        {

            return new BaseServiceResponse<Company> { ExceptionList = new List<string> { ex.Message } , Success = false};
        }


    }
}
