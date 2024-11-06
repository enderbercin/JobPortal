using JobPortal.Domain.Entities;
using JobPortal.Infrastructure.Repository.Abstracts;
using MediatR;

namespace JobPortal.Application.Queries.Companies;

public class GetCompanyByIdQuery : IRequest<Company>
{
    public Guid Id { get; set; }

    public GetCompanyByIdQuery(Guid id)
    {
        Id = id;
    }
}
public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, Company>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCompanyByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Company> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.GetRepository<Company>().GetByIdAsync(request.Id);
    }
}
