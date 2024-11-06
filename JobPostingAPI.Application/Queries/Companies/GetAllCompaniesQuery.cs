using JobPortal.Domain.Entities;
using JobPortal.Infrastructure.Repository.Abstracts;
using MediatR;

namespace JobPortal.Application.Queries.Companies;

public class GetAllCompaniesQuery : IRequest<List<Company>> 
{

}
public class GetAllCompaniesQueryHandler : IRequestHandler<GetAllCompaniesQuery, List<Company>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCompaniesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Company>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
    {
        var resp = await _unitOfWork.GetRepository<Company>().GetAllAsync();
        return resp.ToList();
    }
}
