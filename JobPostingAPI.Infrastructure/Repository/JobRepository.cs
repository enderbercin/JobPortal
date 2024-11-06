using JobPortal.Domain.Entities;
using JobPortal.Infrastructure.Data;
using JobPortal.Infrastructure.Repository.Abstracts;

namespace JobPortal.Infrastructure.Repository;

public class JobRepository :Repository<Job> ,IJobRepository
{
    private readonly JobPortalDbContext _context;

    public JobRepository(JobPortalDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Job> GetByIdAsync(Guid id) =>
        await _context.Jobs.FindAsync(id);

    public async Task AddAsync(Job job) =>
        await _context.Jobs.AddAsync(job);

    public void Update(Job job) =>
        _context.Jobs.Update(job);

    public async Task DeleteAsync(Guid id)
    {
        var job = await _context.Jobs.FindAsync(id);
        if (job != null)
        {
            _context.Jobs.Remove(job);
        }
    }
}
