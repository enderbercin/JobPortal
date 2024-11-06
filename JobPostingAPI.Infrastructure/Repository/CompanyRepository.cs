using JobPortal.Domain.Entities;
using JobPortal.Infrastructure.Data;
using JobPortal.Infrastructure.Repository.Abstracts;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace JobPortal.Infrastructure.Repository;

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

    public async Task<Company> GetByPhoneNumber(string number)
    {
        string cleanNumber = Regex.Replace(number, @"[^\d]", "");
        string last10Digits = cleanNumber.Substring(cleanNumber.Length - 10);
        return await _context.Companies
                                   .FirstOrDefaultAsync(c => c.PhoneNumber.EndsWith(last10Digits));
    }
}
