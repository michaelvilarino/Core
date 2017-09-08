﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dommel;
using System.Linq.Expressions;
using System.Data;
using Core.Util;

namespace Core.Connection
{
    public class SqlConnectionCore : IDisposable
    {
        private SqlConnection sqlConnection;

        public SqlTransaction Transaction;

        public SqlConnectionCore(string connectionString)
        {
            sqlConnection = new SqlConnection(connectionString);
        }

        #region Simples Select

        public IEnumerable<T> QueryTSql<T>(string Sql, object param) where T : class
        {
            if (Transaction != null)
                return sqlConnection.Query<T>(Sql, param, Transaction);
            else
                return sqlConnection.Query<T>(Sql, param);
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            return sqlConnection.GetAll<T>();
        }

        public T Get<T>(object Id) where T : class
        {
            return sqlConnection.Get<T>(Id);
        }

        public IEnumerable<T> Select<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return sqlConnection.Select<T>(predicate);
        }

        #endregion Simples Select

        #region Select Com Join (Limitado até 7 tabelas)

        public IEnumerable<TReturn> SelectJoin<TFirst, TSecond, TReturn>(Func<TFirst, TSecond, TReturn> FuncKey)
              where TReturn : class
        {
            return sqlConnection.GetAll<TFirst, TSecond, TReturn>(FuncKey);
        }

        public IEnumerable<TReturn> SelectJoin<TFirst, TSecond, TThird, TReturn>(Func<TFirst, TSecond, TThird, TReturn> FuncKey)
            where TReturn : class
        {
            return sqlConnection.GetAll<TFirst, TSecond, TThird, TReturn>(FuncKey);
        }

        public IEnumerable<TReturn> SelectJoin<TFirst, TSecond, TThird, TFourth, TReturn>(Func<TFirst, TSecond, TThird, TFourth, TReturn> FuncKey)
          where TReturn : class
        {
            return sqlConnection.GetAll<TFirst, TSecond, TThird, TFourth, TReturn>(FuncKey);
        }

        public IEnumerable<TReturn> SelectJoin<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> FuncKey)
          where TReturn : class
        {
            return sqlConnection.GetAll<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(FuncKey);
        }

        public IEnumerable<TReturn> SelectJoin<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> FuncKey)
         where TReturn : class
        {
            return sqlConnection.GetAll<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(FuncKey);
        }

        public IEnumerable<TReturn> SelectJoin<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> FuncKey)
           where TReturn : class
        {
            return sqlConnection.GetAll<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(FuncKey);
        }

        #endregion Select Com Join (Limitado até 7 tabelas)

        #region CRUD

        public long Insert<T>(T entity) where T : class
        {
            object Id = 0;

            if (Transaction != null)
                Id = sqlConnection.Insert<T>(entity, Transaction);
            else
                Id = sqlConnection.Insert<T>(entity);

            return Convert.ToInt64(Id);
        }

        public bool Update<T>(T entity) where T : class
        {
            if (Transaction != null)
                return sqlConnection.Update<T>(entity, Transaction);
            else
                return sqlConnection.Update<T>(entity);
        }

        public bool Delete<T>(T entity) where T : class
        {
            if (Transaction != null)
                return sqlConnection.Delete<T>(entity, Transaction);
            else
                return sqlConnection.Delete<T>(entity);
        }

        #endregion CRUD

        #region Controles do SqlConnection Nativo
        public void BeginTransaction()
        {
            try
            {
                if (sqlConnection.Equals(null))
                    throw new Exception("A conexão não está ativa");

                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                Transaction = sqlConnection.BeginTransaction();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public ConnectionState connectionState()
        {
            return sqlConnection.State;
        }

        public void ChangeDataBase(string DataBase)
        {
            sqlConnection.ChangeDatabase(DataBase);
        }

        public void CommitTransaction()
        {
            try
            {
                if (sqlConnection.Equals(null))
                    throw new Exception("A conexão não está ativa");

                if (Transaction.Equals(null))
                    throw new Exception("A transação não foi iniciada");

                Transaction.Commit();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void RollBackTransaction()
        {
            try
            {
                if (sqlConnection.Equals(null))
                    throw new Exception("A conexão não está ativa");

                if (Transaction.Equals(null))
                    throw new Exception("A transação não foi iniciada");

                Transaction.Rollback();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Close()
        {
            if (sqlConnection != null)
                sqlConnection.Close();
        }

        public void Open()
        {
            if (sqlConnection != null)
                sqlConnection.Open();
        }

        #endregion Controles do SqlConnection Nativo

        public void Dispose()
        {
            if (sqlConnection != null)
            {
                sqlConnection.Close();
                sqlConnection.Dispose();
                GC.SuppressFinalize(sqlConnection);
            }
        }

    }
}
