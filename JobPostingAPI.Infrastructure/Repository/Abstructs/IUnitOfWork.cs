using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Infrastructure.Repository.Abstructs
{
    public interface IUnitOfWork
    {
        ICompanyRepository CompanyRepository { get; }
        IJobRepository JobRepository { get; }
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync();

    }
}
