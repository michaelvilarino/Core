using Core.Connection;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.BaseRepository
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {        
        private SqlConnectionCore _sqlConnectionCore;

        public RepositoryBase(SqlConnectionCore sqlConnectionCore)
        {
            _sqlConnectionCore = sqlConnectionCore;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _sqlConnectionCore.GetAll<TEntity>();
        }

        public TEntity GetById(long id)
        {
            return _sqlConnectionCore.Get<TEntity>(id);
        }

        public IEnumerable<TEntity> GetListWithPredicate(Expression<Func<TEntity, bool>> predicate)
        {
            return _sqlConnectionCore.Select<TEntity>(predicate);
        }

        public long Insert(TEntity entity)
        {
            return _sqlConnectionCore.Insert<TEntity>(entity);
        }

        public bool Update(TEntity entity)
        {
            return _sqlConnectionCore.Update<TEntity>(entity);
        }

        public bool Delete(TEntity entity)
        {
            return _sqlConnectionCore.Delete<TEntity>(entity);
        }
    }
}
