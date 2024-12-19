using System.Data.Common;

namespace Services.TransactionManagement
{
    public class TransactionManager
    {
        private readonly DbConnection _connection;

        public TransactionManager(DbConnection connection)
        {
            _connection = connection;
        }

        public async Task ExecuteTransactionAsync(Func<DbTransaction, Task> operation)
        {
            DbTransaction transaction = null;

            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                    await _connection.OpenAsync();

                transaction = _connection.BeginTransaction();

                await operation(transaction);

                transaction.Commit();
            }
            catch
            {
                transaction?.Rollback();
                throw;
            }
            finally
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                    _connection.Close();
            }
        }
    }
}