using Core.Enums;
using JobPortal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Services.Abstructs
{
    public interface IJobQualityService
    {
        public int CalculateQualityScore(EmploymentType? employmentType, decimal? salary, string? benefits, string description);
    }
}
