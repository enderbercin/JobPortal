using JobPortal.Domain.Abstructs;
using JobPortal.Domain.Entities;
using JobPortal.Infrastructure.Data;
using JobPortal.Infrastructure.Repository;
using JobPortal.Infrastructure.Repository.Abstructs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Infrastructure
{
    public class UnitOfWork<TEntity> : IUnitOfWork<TEntity> where TEntity : BaseEntity
    {
        private readonly JobPortalDbContext _context;

        public UnitOfWork(JobPortalDbContext appDbContext)
        {
            _context = appDbContext;
        }

        private JobRepository _JobRepository;
        public IJobRepository Jobs => _JobRepository = _JobRepository ?? new JobRepository(_context);


        private Repository<TEntity> _repository;
        public IRepository<TEntity> Repositories => _repository = _repository ?? new Repository<TEntity>(_context);

        public IRepository<Company> Companies => throw new NotImplementedException();

        IRepository<Job> IUnitOfWork<TEntity>.Jobs => throw new NotImplementedException();

        public Task CommitAsync()
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public Task<int> CompleteAsync()
        {
            throw new NotImplementedException();
        }
    }
}
