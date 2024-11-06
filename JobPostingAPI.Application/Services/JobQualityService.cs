using Core.Enums;
using JobPortal.Application.Services.Abstracts;

namespace JobPortal.Application.Services;

public class JobQualityService : IJobQualityService
{
    private readonly List<string> _restrictedWords = new List<string> { "yasaklı1", "yasaklı2", "yasaklı3" };

    public int CalculateQualityScore(EmploymentType? employmentType, decimal? salary, string? benefits, string description)
    {
        int score = 0;

        if (employmentType.HasValue) score += 1;
        if (salary.HasValue) score += 1;
        if (!string.IsNullOrEmpty(benefits)) score += 1;
        if (!_restrictedWords.Any(word => description.Contains(word))) score += 2;

        return score;
    }
}
