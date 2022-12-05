using Data;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ContextDB Context;
        private IDbContextTransaction _transaction;
        public Dictionary<Type, object> repositories = new Dictionary<Type, object>();
        private bool disposed = false;

        public UnitOfWork(ContextDB context)
        {
            this.Context = context;
            this._transaction = this.Context.Database.BeginTransaction();
        }
        public IGenericRepository<T> GenericRepository<T>() where T : class
        {
            if (repositories.Keys.Contains(typeof(T)) == true)
            {
                return repositories[typeof(T)] as IGenericRepository<T>;
            }
            IGenericRepository<T> repo = new GenericRepository<T>(Context);
            repositories.Add(typeof(T), repo);
            return repo;
        }
        public void Save()
        {
            try
            {
                this._transaction = this.Context.Database.BeginTransaction();
                if (this._transaction != null)
                {
                    this.Context.SaveChanges();
                    this._transaction.Commit();
                    this._transaction = null;
                }
            }
            catch (Exception)
            {
                this._transaction.Rollback();
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction = null;
            }
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void SaveTransaction()
        {
            try
            {
                this._transaction.Rollback();
                this._transaction = this.Context.Database.BeginTransaction();
                if (this._transaction != null)
                {
                    this.Context.SaveChanges();
                    this._transaction.Commit();
                    this._transaction = null;
                }
            }
            catch (Exception e)
            {
                this._transaction.Rollback();
            }
        }
    }
}
