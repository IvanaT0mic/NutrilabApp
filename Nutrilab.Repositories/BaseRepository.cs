using Microsoft.EntityFrameworkCore;
using Nutrilab.DataAccess.Context;

namespace Nutrilab.Repositories
{
    public abstract class BaseRepository<TEntity>(EntityContext _entityContext)
           where TEntity : class, new()
    {
        protected readonly DbSet<TEntity> _context = _entityContext.Set<TEntity>();

        public IQueryable<TEntity> GetQueryable()
        {
            var query = _context.AsQueryable();
            return query;
        }

        public async Task<TEntity> InsertAsync(TEntity data)
        {
            var dbEntity = GetFlatValues(data);
            _context.Add(dbEntity);
            try
            {
                await _entityContext.SaveChangesAsync();
                _context.Entry(dbEntity).State = EntityState.Detached;
            }
            catch
            {
                _context.Remove(dbEntity);
                throw;
            }

            return dbEntity;
        }

        public async Task<List<TEntity>> InsertRangeAsync(List<TEntity> data)
        {
            var addedToContext = new List<TEntity>();
            foreach (var item in data)
            {
                var dbEntity = GetFlatValues(item);
                _context.Add(dbEntity);
                addedToContext.Add(dbEntity);
            }

            try
            {
                await _entityContext.SaveChangesAsync();

                foreach (var item in addedToContext)
                {
                    _context.Entry(item).State = EntityState.Detached;
                }
            }
            catch
            {
                _context.RemoveRange(addedToContext);
                throw;
            }
            return addedToContext;
        }

        public async Task<TEntity> UpdateAsync(TEntity updateObject)
        {
            TEntity? addedToContext = null;
            try
            {
                var databaseObject = GetFlatValues(updateObject);
                _context.Attach(databaseObject);
                addedToContext = databaseObject;
                _entityContext.Entry(databaseObject).State = EntityState.Modified;

                await _entityContext.SaveChangesAsync();
                return databaseObject;
            }
            finally
            {
                if (addedToContext != null)
                {
                    _context.Entry(addedToContext).State = EntityState.Detached;
                }
            }
        }

        public async Task UpdateRangeAsync(List<TEntity> data)
        {
            var addedToContext = new List<TEntity>();
            try
            {
                foreach (var item in data)
                {
                    var databaseObject = GetFlatValues(item);
                    addedToContext.Add(databaseObject);

                    _context.Attach(databaseObject);
                    _entityContext.Entry(databaseObject).State = EntityState.Modified;
                }
                await _entityContext.SaveChangesAsync();
            }
            finally
            {
                foreach (var item in addedToContext)
                {
                    _context.Entry(item).State = EntityState.Detached;
                }
            }
        }

        public async Task DeleteAsync(TEntity data)
        {
            TEntity? addedToContext = null;
            try
            {
                var databaseObject = GetFlatValues(data);
                addedToContext = databaseObject;

                _context.Attach(databaseObject);

                _entityContext.Entry(databaseObject).State = EntityState.Deleted;
                await _entityContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                if (addedToContext != null)
                {
                    _context.Entry(addedToContext).State = EntityState.Detached;
                }
                throw;
            }
        }

        public async Task DeleteRangeAsync(List<TEntity> data)
        {
            var addedToContext = new List<TEntity>();
            try
            {
                foreach (var item in data)
                {
                    var databaseObject = GetFlatValues(item);
                    _context.Attach(databaseObject);
                    addedToContext.Add(databaseObject);
                    _entityContext.Entry(databaseObject).State = EntityState.Deleted;
                }
                await _entityContext.SaveChangesAsync();
            }
            finally
            {
                foreach (var item in addedToContext)
                {
                    _context.Entry(item).State = EntityState.Detached;
                }
            }
        }

        private TEntity GetFlatValues(TEntity source)
        {
            var target = new TEntity();
            var properties = _context.Entry(target).Properties;
            foreach (var propertyEntry in properties)
            {
                var property = propertyEntry.Metadata;
                // Skip shadow and key properties
                if (property.IsShadowProperty())
                    continue;
                propertyEntry.CurrentValue = property.GetGetter().GetClrValue(source);
            }
            return target;
        }
    }
}
