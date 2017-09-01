using Core.MappingClass;
using Dapper.FluentMap.Dommel.Mapping;
using Dommel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace UTestProject.Map
{
    public class GenericObjectMap<T> : DommelEntityMap<T> where T: class
    {
        private MapClass<T> _map;
        private string tableName;
        private string columnName;        

        public GenericObjectMap()
        {            
            _map = new MapClass<T>();

            tableName = _map.Table;
            ToTable(tableName);

            var typeClass = typeof(T);                      

            if (_map.PkCollection.Count == 0 && _map.PkAutoIncrementCollection.Count == 0)
                throw new Exception("Nenhuma chave primária foi definida");

            if (_map.PkCollection.Count > 0 && _map.PkCollection.Count > 0)
                throw new Exception("A definição da chave primaria está incorreta!");

            foreach (var column in _map.PkCollection)
            {
                var lambda = generateLambdaFromPropertyInfo(column, typeClass);
                columnName = column.Key;

                Map(lambda).ToColumn(columnName).IsKey();
            }

            foreach (var column in _map.PkAutoIncrementCollection)
            {
                var lambda = generateLambdaFromPropertyInfo(column, typeClass);
                columnName = column.Key;

                Map(lambda).ToColumn(columnName).IsKey().IsIdentity();
            }                     

            foreach (var column in _map.DBColumnCollection)
            {
                var lambda = generateLambdaFromPropertyInfo(column, typeClass);
                columnName = column.Key;

                Map(lambda).ToColumn(columnName);                                
            }

            foreach (var column in _map.DBColumnForeignKeyCollection)
            {
                var lambda = generateLambdaFromPropertyInfo(column, typeClass);
                columnName = column.Key;

                Map(lambda).ToColumn(columnName).IsKey();
            }

            //var a = new CustomKeyPropertyResolver();
            //a.ResolveKeyProperty(typeClass);

            //DommelMapper.SetKeyPropertyResolver(a);
        }

        private Expression<Func<T, object>> generateLambdaFromPropertyInfo(KeyValuePair<string, PropertyInfo> column, Type typeClass)
        {
            var parameter = Expression.Parameter(typeClass);
            var property = Expression.Property(parameter, column.Value);
            var conversion = Expression.Convert(property, typeof(object));

            return  Expression.Lambda<Func<T, object>>(conversion, parameter);
        }

    }

    public class CustomKeyPropertyResolver : DommelMapper.IKeyPropertyResolver
    {
        public PropertyInfo ResolveKeyProperty(Type type)
        {
            return type.GetProperties().Single(p => p.Name == $"{type.Name}Id");
        }

        public PropertyInfo ResolveKeyProperty(Type type, out bool isIdentity)
        {
            isIdentity = false;
            return type.GetProperties().Single(p => p.Name == $"{type.Name}");
        }
    }
}
