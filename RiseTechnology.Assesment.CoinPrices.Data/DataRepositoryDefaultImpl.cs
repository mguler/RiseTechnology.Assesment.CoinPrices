using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RiseTechnology.Assesment.CoinPrices.Core.Abstract.Data;

namespace RiseTechnology.Assesment.CoinPrices.Data
{
    /// <summary>
    /// The default implementation of "IDataRepository" interface that supplies database access and basic functionality
    /// </summary>
    public class DataRepositoryDefaultImpl :IDataRepository
    {
        private readonly DbContext _dataContext;
        private readonly Dictionary<string, IClrPropertyGetter> _primaryKeyCache = new Dictionary<string, IClrPropertyGetter>();

        public DataRepositoryDefaultImpl(DbContext dataContext)
        {
            _dataContext = dataContext;
        }
        public IQueryable<T> Get<T>() where T : class => _dataContext.Set<T>();
        public T Save<T>(T entity) where T : class
        {
            var primaryKey = GetPrimaryKey<T>();

            if (primaryKey.HasDefaultValue(entity))
            {
                _dataContext.Add(entity);
            }
            else
            {
                _dataContext.Update(entity);
            }

            _dataContext.SaveChanges();
            return entity;
        }
        private IClrPropertyGetter GetPrimaryKey<T>()
        {
            var key = typeof(T).FullName;
            var getter = default(IClrPropertyGetter);

            if (_primaryKeyCache.ContainsKey(key))
            {
                getter = _primaryKeyCache[key];
            }
            else
            {
                var primaryKey = _dataContext.Model.GetEntityTypes()?.FirstOrDefault(entityType => entityType.ClrType.FullName == key)?.FindPrimaryKey();
                if (primaryKey != null) 
                {
                    getter = primaryKey.Properties.FirstOrDefault()?.GetGetter();
                    _primaryKeyCache.Add(key, getter);
                }
            }

            if (getter is null) {
                throw new Exception($"There is no primary key defined for the type {typeof(T)}");
            }

            return getter;
        }
        public void Dispose()
        {
            _dataContext.Dispose();
        }
    }
}
