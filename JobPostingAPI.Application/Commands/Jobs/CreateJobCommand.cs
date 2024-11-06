using Core.Enums;
using JobPortal.Application.Services.Abstructs;
using JobPortal.Domain;
using JobPortal.Domain.Entities;
using JobPortal.Infrastructure.Repository.Abstructs;
using MediatR;

namespace JobPortal.Application.Commands.Jobs
{
    public class CreateJobCommand : IRequest<BaseServiceResponse>
    {
        public Guid CompanyId { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public EmploymentType? EmploymentType { get; set; }
        public decimal? Salary { get; set; }
        public string? Benefits { get; set; }
    }
    public class CreateJobHandler : IRequestHandler<CreateJobCommand, BaseServiceResponse>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IJobQualityService _jobQualityService;

        public CreateJobHandler(ICompanyRepository companyRepository, IJobRepository jobRepository, IJobQualityService jobQualityService)
        {
            _companyRepository = companyRepository;
            _jobRepository = jobRepository;
            _jobQualityService = jobQualityService;
        }

        public async Task<BaseServiceResponse> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {

            var company = await _companyRepository.GetByIdAsync(request.CompanyId);
            if (company == null || company.PostingQuota <= 0)
            {
                var message = "İlan yayınlama hakkınız bulunmamaktadır.";
                var resp = new BaseServiceResponse()
                {
                    Status = 400,
                    Success = false,
                    ExceptionList = new List<string>() { message }
                };
                return resp;
            }

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
            await _companyRepository.UpdateAsync(company);
            var response = new BaseServiceResponse<Job>()
            {
                Status = 200,
                Success = true,
                Data = job

            };
            return response;
        }
    }
}
