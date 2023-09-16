namespace RiseTechnology.Assesment.CoinPrices.Core.Abstract.Data
{
    /// <summary>
    /// This abstraction is a contract for implement a service that supplies database operations
    /// </summary>
    public interface IDataRepository : IDisposable
    {
        /// <summary>
        /// This method provides accessibility to the database tables for read operations
        /// </summary>
        /// <typeparam name="T">A clr type which refers to a database table</typeparam>
        /// <returns>A collection that implemets IQueryable and contains data from related table</returns>
        IQueryable<T> Get<T>() where T : class;
        /// <summary>
        /// This method provides accessibility to the database tables for insert,update and delete operations
        /// </summary>
        /// <typeparam name="T">A clr type which refers to a database table</typeparam>
        /// <param name="entity">An object that contains data</param>
        /// <returns>An object that contains data</returns>
        T Save<T>(T entity) where T : class;
    }
}
