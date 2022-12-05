namespace Repository
{
    using Data;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using System;
using System.Collections.Generic;
    using System.Data.Common;
    using System.Linq;
    using System.Linq.Expressions;


    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal ContextDB context;
        internal DbSet<TEntity> dbSet;
        public GenericRepository(ContextDB context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }
        public virtual IEnumerable<TEntity> Get(
             Expression<Func<TEntity, bool>> filter = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
             string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
        public virtual TEntity FindSingleBy(
         Expression<Func<TEntity, bool>> filter = null)
        {
            TEntity query = null;
            if (filter != null)
            {
                return query = context.Set<TEntity>().Where(filter).SingleOrDefault();
            }
            else
            {
                throw new ArgumentNullException("Predicate value must be passed to FindSingleBy<T>.");
            }
        }
        public virtual IEnumerable<TEntity> GetAllBy(
         Expression<Func<TEntity, bool>> filter = null)
        {
            IEnumerable<TEntity> query = null;
            if (filter != null)
            {
                return query = context.Set<TEntity>().Where(filter);
            }
            else
            {
                throw new ArgumentNullException("Predicate value must be passed to FindSingleBy<T>.");
            }
        }
        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }
        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }
        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }
        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }
        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Update(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        public virtual void UpdateRange(List<TEntity> entityToUpdates)
        {
            dbSet.UpdateRange(entityToUpdates);
        }
        public virtual DbCommand LoadStoredProc(string storedProcName)
        {
            var cmd = context.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = storedProcName;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            return cmd;
        }
        public Int32 ExecuteStoreProcedure<T>(String procedure, List<SqlParameter> parameters) where T : new()
        {
            Int32 res;
            using (var connection = context.Database.GetDbConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = procedure;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters.ToArray<SqlParameter>());
                    res = command.ExecuteNonQuery();
                }
            }
            return res;
        }
    }
}
