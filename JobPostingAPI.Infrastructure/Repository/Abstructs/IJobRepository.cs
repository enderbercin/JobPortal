﻿using JobPortal.Domain.Abstructs;
using JobPortal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Infrastructure.Repository.Abstructs
{
    public interface IJobRepository : IRepository<Job>
    {
    }
}