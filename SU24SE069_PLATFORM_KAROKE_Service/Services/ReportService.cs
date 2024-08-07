using Microsoft.EntityFrameworkCore;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.Services
{
    public class ReportService : IReportRepository
    {
        public Task<bool> AddReport(Report report)
        {
            throw new NotImplementedException();
        }

        public bool Any(Func<Report, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void AttrachEntity(Report entity)
        {
            throw new NotImplementedException();
        }

        public int Count(Func<Report, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public void Delete(Report entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(IQueryable<Report> entities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteReport(Report report)
        {
            throw new NotImplementedException();
        }

        public void DetachEntity(Report entity)
        {
            throw new NotImplementedException();
        }

        public Task DisponseAsync()
        {
            throw new NotImplementedException();
        }

        public Report Find(Func<Report, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Report> FindAll(Func<Report, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Report> FindAsync(Expression<Func<Report, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Report? FindEntity(params object[] data)
        {
            throw new NotImplementedException();
        }

        public Task<Report> FirstOrDefaultAsync(Expression<Func<Report, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Report FistOrDefault(Func<Report, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public DbSet<Report> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Report> GetAll(Expression<Func<Report, bool>>? filter = null, Func<IQueryable<Report>, IOrderedQueryable<Report>>? orderBy = null, string? includeProperties = null)
        {
            throw new NotImplementedException();
        }

        public Task<Report> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Report> GetByIdGuid(Guid id)
        {
            throw new NotImplementedException();
        }

        public DbSet<Report> GetDbSet()
        {
            throw new NotImplementedException();
        }

        public Report GetFirstOrDefault(Expression<Func<Report, bool>>? filter = null, string? includeProperties = null)
        {
            throw new NotImplementedException();
        }

        public Report GetMax()
        {
            throw new NotImplementedException();
        }

        public Report GetMax(Func<Report, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Report GetMin()
        {
            throw new NotImplementedException();
        }

        public Report GetMin(Func<Report, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Report>> GetWhere(Expression<Func<Report, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task HardDelete(int key)
        {
            throw new NotImplementedException();
        }

        public Task HardDeleteGuid(Guid key)
        {
            throw new NotImplementedException();
        }

        public void Insert(Report entity)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(Report entity)
        {
            throw new NotImplementedException();
        }

        public void InsertRangeAsync(IQueryable<Report> entities)
        {
            throw new NotImplementedException();
        }

        public bool IsMax(Func<Report, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsMaxAsync(Expression<Func<Report, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public bool IsMin(Func<Report, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsMinAsync(Expression<Func<Report, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void MotifyEntity(Report entity)
        {
            throw new NotImplementedException();
        }

        public void SaveChages()
        {
            throw new NotImplementedException();
        }

        public Task SaveChagesAsync()
        {
            throw new NotImplementedException();
        }

        public Task Update(Report entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateById(Report entity, int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateGuid(Report entity, Guid id)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(IQueryable<Report> entities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateReport(Report report)
        {
            throw new NotImplementedException();
        }
    }
}
