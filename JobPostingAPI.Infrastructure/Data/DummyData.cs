using JobPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JobPortal.Infrastructure.Data
{
    public static class DummyData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new JobPortalDbContext(serviceProvider.GetRequiredService<DbContextOptions<JobPortalDbContext>>());

            if (!context.Companies.Any())
            {
                var companies = new[]
                {
                    new Company
                    {
                        Name = "Tech Innovations Ltd",
                        PhoneNumber = "123-456-7890",
                        Address = "123 Tech Lane, Silicon Valley, CA",
                        PostingQuota = 2,
                        RecordStatus = RecordStatus.Active
                    },
                    new Company
                    {
                        Name = "Creative Solutions Inc",
                        PhoneNumber = "987-654-3210",
                        Address = "456 Creative Rd, New York, NY",
                        PostingQuota = 3,
                        RecordStatus = RecordStatus.Active

                    },
                    new Company
                    {
                        Name = "Global Services LLC",
                        PhoneNumber = "555-123-4567",
                        Address = "789 Global St, London, UK",
                        PostingQuota = 1,
                        RecordStatus = RecordStatus.Active

                    }
                };

                context.Companies.AddRange(companies);
                context.SaveChanges();

                var techCompany = companies.First();
                var creativeCompany = companies.Skip(1).First();

                var jobs = new[]
                {
                    CreateJob(techCompany.Id, "Software Developer", "Develop scalable web applications using .NET Core.", "Full-time", 60000m, "Health insurance, Retirement plan"),
                    CreateJob(techCompany.Id, "Systems Analyst", "Analyze and improve IT systems.", null, null, null),
                    CreateJob(creativeCompany.Id, "UI/UX Designer", "Design user-friendly interfaces.", "Part-time", 45000m, "Flexible hours"),
                    CreateJob(creativeCompany.Id, "Project Manager", "Manage software projects from start to finish.", "Contract", 70000m, "Health insurance")
                    };

                context.Jobs.AddRange(jobs);
                context.SaveChanges();
            }
        }

        private static Job CreateJob(Guid companyId, string position, string description, string? employmentType, decimal? salary, string? benefits)
        {
            var qualityScore = CalculateQualityScore(employmentType, salary, benefits, description);
            return new Job
            {
                CompanyId = companyId,
                Position = position,
                Description = description,
                EmploymentType = employmentType,
                Salary = salary,
                Benefits = benefits,
                QualityScore = qualityScore
            };
        }

        private static int CalculateQualityScore(string? employmentType, decimal? salary, string? benefits, string description)
        {
            int score = 0;

            // Çalışma türü belirtilmesi (1 puan)
            if (!string.IsNullOrEmpty(employmentType)) score += 1;

            // Ücret bilgisi belirtilmesi (1 puan)
            if (salary.HasValue) score += 1;

            // Yan haklar belirtilmesi (1 puan)
            if (!string.IsNullOrEmpty(benefits)) score += 1;

            // İlan açıklamasında sakıncalı kelime bulunmaması (2 puan)
            // Örnek sakıncalı kelimeler
            string[] inappropriateWords = { "bad", "terrible", "awful" };
            if (!inappropriateWords.Any(word => description.Contains(word, StringComparison.OrdinalIgnoreCase)))
            {
                score += 2;
            }

            return score;
        }
    }
}


