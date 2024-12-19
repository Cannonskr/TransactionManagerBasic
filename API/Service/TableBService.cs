using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using API.Model;
using Microsoft.Data.SqlClient;

namespace API.Service
{
    public class TableBService : ITableBService
    {
        private readonly DbConnection _connection;

        public TableBService(DbConnection connection)
        {
            _connection = connection;
        }
        public async Task InsertDataAsync(TableBDataModel data, DbTransaction transaction)
        {
            var command = _connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = "INSERT INTO TableB (Id, Name, NickName) VALUES (@Id, @Name, @NickName)";

            command.Parameters.Add(new SqlParameter("@Id", data.Id));
            command.Parameters.Add(new SqlParameter("@Name", data.Name));
            command.Parameters.Add(new SqlParameter("@NickName", data.NickName));

            await command.ExecuteNonQueryAsync();
            
        }
    }
}