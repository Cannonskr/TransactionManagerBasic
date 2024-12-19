using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using API.Model;
using Microsoft.Data.SqlClient;

namespace API.Service
{
    public class TableAService : ITableAService
    {
        private readonly DbConnection _connection;

        public TableAService(DbConnection connection)
        {
            _connection = connection;
        }
        public async Task InsertDataAsync(TableADataModel data, DbTransaction transaction)
        {
            var command = _connection.CreateCommand();
            command.Transaction = transaction  ;

            command.CommandText = "INSERT INTO TableA (Id, Name) VALUES (@Id, @Name)";
            command.Parameters.Add(new SqlParameter("@Id", data.Id));
            command.Parameters.Add(new SqlParameter("@Name", data.Name));


            await command.ExecuteNonQueryAsync();

        }
    }
}