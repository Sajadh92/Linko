using Microsoft.Data.SqlClient;
using System;
using System.Data.Common;
using Dapper;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Linko.Helper
{
    public class DapperRepository<TEntity> : IDapperRepository<TEntity>
    {
        private static DbConnection Connection;

        public DapperRepository()
        {
            Connection = new SqlConnection(DBConn.ConnectionString);

            try
            {
                Connection.Open();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public void RunScript(string Query)
        {
            try
            {
                Connection.Query(Query, commandTimeout: 300,
                    commandType: CommandType.Text);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task RunScriptAsync(string Query)
        {
            try
            {
                 await Connection.QueryAsync(Query, commandTimeout: 300,
                    commandType: CommandType.Text);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RunSp(string spName, object pars)
        {
            try
            {
                Connection.Query(spName, pars, commandTimeout: 300,
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task RunSpAsync(string spName, object pars)
        {
            try
            {
                await Connection.QueryAsync(spName, pars, commandTimeout: 300,
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TEntity GetEntity(string spName, object pars)
        {
            try
            {
                TEntity result = Connection.QueryFirst<TEntity>(spName, pars,
                   commandTimeout: 300, commandType: CommandType.StoredProcedure);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<TEntity> GetEntityAsync(string spName, object pars)
        {
            try
            {
                TEntity result = await Connection.QueryFirstAsync<TEntity>(spName, pars,
                   commandTimeout: 300, commandType: CommandType.StoredProcedure);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<TEntity> GetEntityList(string spName, object pars)
        {
            try
            {
                IEnumerable<TEntity> result = Connection.Query<TEntity>(spName, pars,
                   commandTimeout: 300, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public async Task<List<TEntity>> GetEntityListAsync(string spName, object pars)
        //{
        //    try
        //    {
        //        IEnumerable<TEntity> result = await Connection.QueryAsync<TEntity>(spName, pars,
        //           commandTimeout: 300, commandType: CommandType.StoredProcedure);

        //        return result.ToList();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        public async Task<List<TEntity>> GetEntityListAsync(string spName, object pars)
        {
            try
            {
                IEnumerable<TEntity> result = await Connection.QueryAsync<TEntity>(spName, pars,
                   commandTimeout: 300, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
