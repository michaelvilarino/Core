using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.MappingClass
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class DBTable : Attribute
    {
        public string Table { get; private set; }

        public DBTable(string table)
        {
            Table = table;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DBColumn : Attribute
    {
        public string Column { get; private set; }

        public DBColumn(string column)
        {
            Column = column;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DBColumnFk : Attribute
    {
        public object classFk { get; private set; }

        public DBColumnFk(string columnFk, object ClassFk)
        {
            classFk = ClassFk;
        }
    }

    public class MapClass<T>
    {
        public Type Type { get; private set; }
        public string Table { get; private set; }
        public IDictionary<string,PropertyInfo> Pk { get; set; }
        public IDictionary<string, PropertyInfo> DBColumnCollection { get; set; }

        public MapClass()
        {
            Type = typeof(T);
            GetTableName();
            FillDBColumnCollection();

            Pk = new Dictionary<string, PropertyInfo>();

            Pk.Add(DBColumnCollection["Id"].Name, DBColumnCollection["Id"]);         
        }

        private void GetTableName()
        {
            DBTable entityField = Type.GetCustomAttributes(typeof(DBTable), false).Select(attr => (DBTable)attr).FirstOrDefault();
            Table = entityField.Table;
        }

        private void FillDBColumnCollection()
        {
            DBColumnCollection = new Dictionary<string, PropertyInfo>();

            foreach (PropertyInfo prop in Type.GetProperties())
            {
                DBColumn attributes = prop.GetCustomAttributes(typeof(DBColumn), true).Select(attr => (DBColumn)attr).FirstOrDefault();               

                if(attributes != null)
                  DBColumnCollection.Add(attributes.Column, prop);
            }
        }
    }
}
