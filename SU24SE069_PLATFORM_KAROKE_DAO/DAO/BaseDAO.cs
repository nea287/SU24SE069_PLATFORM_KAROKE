using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SU24SE069_PLATFORM_KAROKE_DAO.IDAO;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_DAO.DAO
{
    public class BaseDAO<TEntity> : IBaseDAO<TEntity> where TEntity : class
    {
        private static BaseDAO<TEntity> instance = null;
        private static readonly object InstanceClock = new object();

        private readonly KokDBContext _context;
        private DbSet<TEntity> Table { get; set; }

        public static BaseDAO<TEntity> Instance
        {
            get
            {
                lock (InstanceClock)
                {
                    if (instance == null)
                    {
                        KokDBContext context = new KokDBContext();
                        instance = new BaseDAO<TEntity>(context);
                    }
                    return instance;
                }
            }
        }

        public BaseDAO(KokDBContext context)
        {
            _context = context;
            Table = context.Set<TEntity>();
        }

        public virtual bool Any(Func<TEntity, bool> predicate)
        {
            return Table.Any(predicate);
        }

        public virtual int Count(Func<TEntity, bool> predicate)
        {
            return Table.Where(predicate).Count();
        }

        public virtual void DeleteRange(IQueryable<TEntity> entities)
        {
            Table.RemoveRange(entities);
        }

        public virtual TEntity Find(Func<TEntity, bool> predicate)
        {
            return Table.Find(predicate);
        }

        public virtual IQueryable<TEntity> FindAll(Func<TEntity, bool> predicate)
        {
            return Table.Where(predicate).AsQueryable();
        }

        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Table.FindAsync(predicate);
        }

        public virtual DbSet<TEntity> GetAll()
        {
            return Table;
        }

        public virtual IQueryable<TEntity> GetAll(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string? includeProperties = null)
        {
            IQueryable<TEntity> query = Table.AsQueryable();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(
                    new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

            }
            if (orderBy != null)
            {
                return orderBy(query).AsQueryable();
            }
            return query.AsQueryable();
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            return await Table.FindAsync(id);
        }

        public async Task<TEntity> GetByIdGuid(Guid id)
        {
            return await Table.FindAsync(id);
        }

        public TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<TEntity> query = Table;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(
                    new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            return query.FirstOrDefault();
        }

        public async Task<IEnumerable<TEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return await Table.Where(predicate).ToListAsync();
        }

        public async Task HardDelete(int key)
        {
            var rs = await GetById(key);
            Table.Remove(rs);
        }

        public async Task HardDeleteGuid(Guid key)
        {
            var rs = await GetByIdGuid(key);
            Table.Remove(rs);
        }

        public void Insert(TEntity entity)
        {
            Table.Add(entity);
        }

        public async Task InsertAsync(TEntity entity)
        {
            await Table.AddAsync(entity);
        }

        public async void InsertRangeAsync(IQueryable<TEntity> entities)
        {
            await Table.AddRangeAsync(entities);
        }

        public void SaveChages()
        {
            _context.SaveChanges();
        }

        public async Task UpdateById(TEntity entity, int id)
        {
            var existedEntity = await GetById(id);
            _context.Entry(existedEntity).CurrentValues.SetValues(entity);
            Table.Update(existedEntity);
        }

        public async Task UpdateGuid(TEntity entity, Guid id)
        {
            var existedEntity = await GetByIdGuid(id);
            _context.Entry(existedEntity).CurrentValues.SetValues(entity);
            Table.Update(entity);
        }

        public async Task Update(TEntity entity)
        {
            _context.Entry(entity).CurrentValues.SetValues(entity);
            Table.Update(entity);
        }

        public void UpdateRange(IQueryable<TEntity> entities)
        {
            Table.UpdateRange(entities);
        }

        public TEntity FistOrDefault(Func<TEntity, bool> predicate)
        {
            return Table.FirstOrDefault(predicate);
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Table.FirstOrDefaultAsync(predicate);
        }

        public void AttrachEntity(TEntity entity)
        {
            _context.Attach(entity);
        }

        public void DetachEntity(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }
        
        public void DetachEntity(IQueryable<TEntity> entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }
        public void MotifyEntity(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public bool IsMin(Func<TEntity, bool> predicate)
        {
            return Table.Min(predicate);
        }

        public bool IsMax(Func<TEntity, bool> predicate)
        {
            return Table.Max(predicate);
        }

        public TEntity GetMin()
        {
            return Table.Min();
        }

        public TEntity GetMax()
        {
            return Table.Max();
        }

        public async Task<bool> IsMaxAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Table.MaxAsync(predicate);
        }

        public async Task<bool> IsMinAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Table.MinAsync(predicate);
        }

        public TEntity GetMin(Func<TEntity, bool> predicate)
        {
            return Table.MinBy(predicate);
        }

        public TEntity GetMax(Func<TEntity, bool> predicate)
        {
            return Table.MaxBy(predicate);
        }

        public int Count()
        {
            return Table.Count();
        }

        public virtual void Delete(TEntity entity)
        {
            Table.Remove(entity);
        }

        public async Task SaveChagesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public TEntity? FindEntity(params object[] data)
        {
            return Table.Find(data);
        }

        public async Task DisponseAsync()
        {
           await _context.DisposeAsync();
            instance = null;


        }

        public DbSet<TEntity> GetDbSet()
        {
            return Table;
        }
    }
}
