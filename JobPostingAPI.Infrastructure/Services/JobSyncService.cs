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
    public class JobSyncService
    {
        private readonly IElasticClient _elasticClient;
        private readonly JobPortalDbContext _context;

        public JobSyncService(IElasticClient elasticClient, JobPortalDbContext context)
        {
            _elasticClient = elasticClient;
            _context = context;
        }

        public async Task SyncJobsToElasticsearch()
        {
            var searchResponse = await _elasticClient.SearchAsync<JobElastic>(s => s
                .Index("jobs")
                .Sort(ss => ss.Descending(p => p.PostedDate))
                .Size(1) 
            );

            DateTime lastPostedDate = searchResponse.Documents.FirstOrDefault()?.PostedDate ?? DateTime.MinValue;

            var jobsToSync = _context.Jobs
                .Where(j => j.CreatedDate > lastPostedDate)
                .ToList();

            var jobElasticDocs = jobsToSync.Select(j => new JobElastic
            {
                Id = j.Id,
                Position = j.Position,
                Description = j.Description,
                PostedDate = j.CreatedDate ?? DateTime.UtcNow,
                ExpirationDate = j.CreatedDate?.AddDays(15) ?? DateTime.UtcNow.AddDays(15),
                QualityScore = j.QualityScore
            }).ToList();

            var bulkResponse = await _elasticClient.BulkAsync(b => b
                .Index("jobs")
                .IndexMany(jobElasticDocs)
            );

            if (bulkResponse.Errors)
            {
                Console.WriteLine("Error syncing jobs to Elasticsearch");
            }
            else
            {
                Console.WriteLine("Jobs successfully synced to Elasticsearch");
            }
        }
    }

}
