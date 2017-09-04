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
    public class Key : Attribute
    {
        public string Column { get; private set; }

        public Key(string column)
        {
            Column = column;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class Key_AutoIncrement : Attribute
    {
        public string Column { get; private set; }

        public Key_AutoIncrement(string column)
        {
            Column = column;
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
    public class DBColumnForeignKey : Attribute
    {
        public string Column { get; private set; }

        public DBColumnForeignKey(string column)
        {
            Column = column;
        }
    }

    public class MapClass<T>
    {
        public Type Type { get; private set; }
        public string Table { get; private set; }
        public IDictionary<string,PropertyInfo>  PkCollection { get; set; }
        public IDictionary<string, PropertyInfo> PkAutoIncrementCollection { get; set; }
        public IDictionary<string, PropertyInfo> DBColumnCollection     { get; set; }
        public IDictionary<string, PropertyInfo> DBColumnForeignKeyCollection { get; set; }

        public MapClass()
        {
            Type = typeof(T);
            GetTableName();
            FillDBColumnCollection();                                          
        }

        private void GetTableName()
        {
            DBTable entityField = Type.GetCustomAttributes(typeof(DBTable), false).Select(attr => (DBTable)attr).FirstOrDefault();
            Table = entityField.Table;
        }

        private void FillDBColumnCollection()
        {
            PkCollection                 = new Dictionary<string, PropertyInfo>();
            PkAutoIncrementCollection    = new Dictionary<string, PropertyInfo>();
            DBColumnCollection           = new Dictionary<string, PropertyInfo>();
            DBColumnForeignKeyCollection = new Dictionary<string, PropertyInfo>();

            foreach (PropertyInfo prop in Type.GetProperties())
            {
                Key attributesKey = prop.GetCustomAttributes(typeof(Key), true).Select(attr => (Key)attr).FirstOrDefault();

                Key_AutoIncrement attributesKey_AutoIncrement = prop.GetCustomAttributes(typeof(Key_AutoIncrement), true).Select(attr => (Key_AutoIncrement)attr).FirstOrDefault();

                DBColumnForeignKey attributesForeignKey = prop.GetCustomAttributes(typeof(DBColumnForeignKey), true).Select(attr => (DBColumnForeignKey)attr).FirstOrDefault();

                DBColumn attributesColumn = prop.GetCustomAttributes(typeof(DBColumn), true).Select(attr => (DBColumn)attr).FirstOrDefault();

                if (attributesColumn != null)
                  DBColumnCollection.Add(attributesColumn.Column, prop);

                if (attributesForeignKey != null)
                {
                    var lengthNameProp = prop.Name.Length;
                    int startPosition = lengthNameProp - 2; 

                    if (prop.Name.Substring(startPosition, 2) != "Id" && prop.Name.Substring(startPosition, 2) != "id")
                        throw new Exception("Uma propriedade foreign Key deve terminar com os caracteres: 'Id' ");
                    else
                        DBColumnForeignKeyCollection.Add(attributesForeignKey.Column, prop);
                }

                if (attributesKey != null)
                  PkCollection.Add(attributesKey.Column, prop);

                if (attributesKey_AutoIncrement != null)
                  PkAutoIncrementCollection.Add(attributesKey_AutoIncrement.Column, prop);
            }
        }
    }
}
