using JobPortal.Application.Services.Abstructs;
using JobPortal.Domain.Entities.Elastic;
using JobPortal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Services
{
    public class CompanySyncService : ICompanySyncService
    {
        private readonly IElasticClient _elasticClient;
        private readonly JobPortalDbContext _context;
        public CompanySyncService(IElasticClient elasticClient, JobPortalDbContext context)
        {
            _elasticClient = elasticClient;
            _context = context;
        }

        public async Task AddOrUpdateCompanyToElastic(Guid companyId)
        {
            var company = await _context.Companies
                                          .Where(c => c.Id == companyId)
                                          .FirstOrDefaultAsync();

            if (company == null) return;

            var companyElastic = new CompanyElastic
            {
                Id = company.Id,
                Name = company.Name,
                PhoneNumber = company.PhoneNumber,
                Address = company.Address,
                PostingQuota = company.PostingQuota,
                RecordStatus = company.RecordStatus,
            };

            var response = await _elasticClient.IndexDocumentAsync(companyElastic);
            if (response.IsValid)
            {
                Console.WriteLine("Company indexed successfully.");
            }
            else
            {
                Console.WriteLine("Error indexing company: " + response.OriginalException.Message);
            }
        }
    }

}
