namespace Repository
{
    using Microsoft.Data.SqlClient;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get(
             Expression<Func<TEntity, bool>> filter = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
             string includeProperties = "");
        TEntity FindSingleBy(
          Expression<Func<TEntity, bool>> filter = null);
        IEnumerable<TEntity> GetAllBy(Expression<Func<TEntity, bool>> filter = null);
        TEntity GetByID(object id);
        void Insert(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
        void UpdateRange(List<TEntity> entityToUpdates);
        Int32 ExecuteStoreProcedure<T>(String query, List<SqlParameter> parameters) where T : new();
    }
}
