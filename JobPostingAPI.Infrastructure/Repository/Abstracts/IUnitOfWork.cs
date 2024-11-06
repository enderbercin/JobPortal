namespace JobPortal.Infrastructure.Repository.Abstracts;

public interface IUnitOfWork
{
    ICompanyRepository CompanyRepository { get; }
    IJobRepository JobRepository { get; }
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync();

}
