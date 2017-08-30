using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.BaseRepository
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(long id);
        TEntity Insert(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
        IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate);        
    }
}
