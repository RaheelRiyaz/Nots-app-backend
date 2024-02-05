using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace NoteTakingApp.Persistence.Dapper
{
    public static class DapperExtensionMethod
    {
        public async static Task<IEnumerable<T>> QueryAsync<T>
            (
            this DbContext dbContext,
            string sql,
            object? param = default,
            IDbTransaction? transaction = default,
            int? commandTimeout = default,
            CommandType commandType = CommandType.Text
            )
        {
            using (SqlConnection connection = new SqlConnection(dbContext.Database.GetConnectionString()))
                return await connection.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }




        public async static Task<T?> FirstOrDefaultAsync<T>
          (
          this DbContext dbContext,
          string sql,
          object? param = default,
          IDbTransaction? transaction = default,
          int? commandTimeout = default,
          CommandType commandType = CommandType.Text
          )
        {
            using (SqlConnection connection = new SqlConnection(dbContext.Database.GetConnectionString()))
                return await connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }



        public async static Task<int> ExecuteAsync
          (
          this DbContext dbContext,
          string sql,
          object? param = default,
          IDbTransaction? transaction = default,
          int? commandTimeout = default,
          CommandType commandType = CommandType.Text
          )
        {
            using (SqlConnection connection = new SqlConnection(dbContext.Database.GetConnectionString()))
                return await connection.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
        }
    }
}
