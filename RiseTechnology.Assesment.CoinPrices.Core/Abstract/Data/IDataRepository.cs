namespace RiseTechnology.Assesment.CoinPrices.Core.Abstract.Data
{
    public interface IDataRepository : IDisposable
    {
        IQueryable<T> Get<T>() where T : class;
        T Save<T>(T entity) where T : class;
        IEnumerable<T> SaveAll<T>(IEnumerable<T> entities) where T : class;
    }
}
