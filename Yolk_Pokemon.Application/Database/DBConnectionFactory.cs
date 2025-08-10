
using Npgsql;
using System.Data;

namespace Yolk_Pokemon.Application.Database
{
    public interface IDbConnectionFactory
    {
        Task<IDbConnection> CreateConnectionAsync(CancellationToken token = default);
    }

    public class NpgsqlConnectionFactory(string connectionSting) : IDbConnectionFactory
    {
        private readonly string _connectionString = connectionSting;

        public async Task<IDbConnection> CreateConnectionAsync(CancellationToken token = default)
        {
            var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync(token);

            return connection;
        }
    }
}