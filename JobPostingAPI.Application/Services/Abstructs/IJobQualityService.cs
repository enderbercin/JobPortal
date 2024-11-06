using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Services.Abstructs
{
    public interface IJobQualityService
    {
        public int CalculateQualityScore(string? employmentType, decimal? salary, string? benefits, string description);
    }
}
