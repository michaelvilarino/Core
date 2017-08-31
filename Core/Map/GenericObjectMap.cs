using Core.MappingClass;
using Dapper.FluentMap.Dommel.Mapping;
using System;
using System.Linq.Expressions;

namespace UTestProject.Map
{
    public class GenericObjectMap<T> : DommelEntityMap<T> where T: class
    {
        private MapClass<T> _map;
        private string tableName;
        private string columnName;        

        public GenericObjectMap()
        {
            bool pkMapped = false;

            _map = new MapClass<T>();

            tableName = _map.Table;
            ToTable(tableName);

            var typeClass = typeof(T);

            foreach (var column in _map.DBColumnCollection)
            {
                var parameter  = Expression.Parameter(typeClass);
                var property   = Expression.Property(parameter, column.Value);
                var conversion = Expression.Convert(property, typeof(object));
                var lambda = Expression.Lambda<Func<T, object>>(conversion, parameter);

                columnName = column.Key;

                if (!pkMapped && column.Key == "Id")
                {
                    Map(lambda).ToColumn(column.Key).IsKey().IsIdentity();
                    pkMapped = true;
                    continue;
                }

                Map(lambda).ToColumn(columnName);                                
            }                      
        }

    }
}
