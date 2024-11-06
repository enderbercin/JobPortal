using JobPortal.Infrastructure.Data;
using JobPortal.Infrastructure.Repository;
using JobPortal.Infrastructure.Repository.Abstracts;

namespace JobPortal.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly JobPortalDbContext _context;
    private Dictionary<Type, object> _repositories;

    public UnitOfWork(JobPortalDbContext context)
    {
        _context = context;
        _repositories = new Dictionary<Type, object>();
    }


    private CompanyRepository _CompanyRepository;

    public ICompanyRepository CompanyRepository => _CompanyRepository = _CompanyRepository ?? new CompanyRepository(_context);

    private JobRepository _JobRepository;

    public IJobRepository JobRepository => _JobRepository = _JobRepository ?? new JobRepository(_context);


    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        if (_repositories.ContainsKey(typeof(TEntity)))
            return (IRepository<TEntity>)_repositories[typeof(TEntity)];

        var repository = new Repository<TEntity>(_context);
        _repositories.Add(typeof(TEntity), repository);
        return repository;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
