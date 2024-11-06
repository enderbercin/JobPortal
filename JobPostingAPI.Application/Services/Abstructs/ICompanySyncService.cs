using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Services.Abstructs
{
    public interface ICompanySyncService
    {
        Task AddOrUpdateCompanyToElastic(Guid companyId);

    }
}
