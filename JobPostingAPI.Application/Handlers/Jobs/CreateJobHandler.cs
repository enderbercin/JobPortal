using JobPortal.Application.Commands.Jobs;
using JobPortal.Application.Services;
using JobPortal.Domain.Entities;
using JobPortal.Infrastructure.Repository.Abstructs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Handlers.Jobs
{
    public class CreateJobHandler : IRequestHandler<CreateJobCommand, Guid>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IJobRepository _jobRepository;
        private readonly JobQualityService _jobQualityService;

        public CreateJobHandler(ICompanyRepository companyRepository, IJobRepository jobRepository, JobQualityService jobQualityService)
        {
            _companyRepository = companyRepository;
            _jobRepository = jobRepository;
            _jobQualityService = jobQualityService;
        }

        public async Task<Guid> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(request.CompanyId);
            if (company == null || company.PostingQuota <= 0)
                throw new Exception("İlan yayınlama hakkınız bulunmamaktadır.");

            var job = new Job
            {
                CompanyId = request.CompanyId,
                Position = request.Position,
                Description = request.Description,
                EmploymentType = request.EmploymentType,
                Salary = request.Salary,
                Benefits = request.Benefits,
                QualityScore = _jobQualityService.CalculateQualityScore(request.EmploymentType, request.Salary, request.Benefits, request.Description)
            };

            await _jobRepository.AddAsync(job);
            company.PostingQuota--; 
            _companyRepository.Update(company);

            return job.Id;
        }
    }
}
