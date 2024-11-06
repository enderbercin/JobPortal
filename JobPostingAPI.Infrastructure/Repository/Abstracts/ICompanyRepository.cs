using JobPortal.Domain.Entities;

namespace JobPortal.Infrastructure.Repository.Abstracts;

public interface ICompanyRepository : IRepository<Company>
{
     Task<Company> GetByPhoneNumber(string number);
}
