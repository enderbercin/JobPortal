using Core.Enums;

namespace JobPortal.Application.Services.Abstracts;

public interface IJobQualityService
{
    public int CalculateQualityScore(EmploymentType? employmentType, decimal? salary, string? benefits, string description);
}
