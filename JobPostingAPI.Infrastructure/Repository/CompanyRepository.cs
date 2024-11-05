using JobPortal.Domain.Entities;
using JobPortal.Infrastructure.Data;
using JobPortal.Infrastructure.Repository.Abstructs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Infrastructure.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly JobPortalDbContext _context;

        public CompanyRepository(JobPortalDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Company> GetByIdAsync(Guid id) =>
            await _context.Companies.Include(c => c.Jobs).FirstOrDefaultAsync(c => c.Id == id);

        public async Task AddAsync(Company company) => await _context.Companies.AddAsync(company);

        public void Update(Company company) => _context.Companies.Update(company);

        public async Task DeleteAsync(Guid id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company != null)
            {
                _context.Companies.Remove(company);
            }
        }
    }
}
