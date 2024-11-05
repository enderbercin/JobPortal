using JobPortal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Domain.Abstructs
{
    public interface IUnitOfWork<TEntity> where TEntity : BaseEntity
    {
        IRepository<Company> Companies { get; }
        IRepository<Job> Jobs { get; }
        Task CommitAsync();
        void Commit();
        Task<int> CompleteAsync();
    }
}
