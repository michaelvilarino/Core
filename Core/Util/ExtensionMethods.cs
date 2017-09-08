using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core.Util
{
    public static class ExtensionMethods
    {
        public static IEnumerable<T> Paginate<T, TResult>
    (
       this IEnumerable<T> objectList,
       int currentPage,
       int pageSize,
       out int pages,
       Expression<Func<T, TResult>> OrderByExpression,
       bool OrderByAsc = false       
    )
    where T : class
        {

            var objectListIQueryable = objectList.AsQueryable();

            int recordCount = objectListIQueryable.Count();
            int skipRows = (currentPage - 1) * pageSize;

            pages = recordCount > 0 ? (int)((recordCount + pageSize) / pageSize) : 1;

            if (recordCount > 0)
            {
                if (OrderByExpression != null)
                {
                    if (OrderByAsc)
                        return objectListIQueryable.OrderByDescending(OrderByExpression).Skip(skipRows)
                                                                                        .Take(pageSize)
                                                                                        .OrderByDescending(OrderByExpression)
                                                                                        .AsEnumerable();
                    else
                        return objectListIQueryable.OrderBy(OrderByExpression).Skip(skipRows)
                                                                              .Take(pageSize)
                                                                              .OrderBy(OrderByExpression)
                                                                              .AsEnumerable();
                }
                else
                    return objectListIQueryable.Skip(skipRows).Take(pageSize).AsEnumerable();
            }
            else
                return null;

        }
    }
}
