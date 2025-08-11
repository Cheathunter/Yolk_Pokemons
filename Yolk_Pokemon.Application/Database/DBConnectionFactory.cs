
using Npgsql;
using System.Data;

namespace Yolk_Pokemon.Application.Database
{
    public interface IDbConnectionFactory
    {
        Task<IDbConnection> CreateConnectionAsync(CancellationToken token = default);
    }

    /// <summary>
    /// Factory to maintain database connection.
    /// </summary>
    /// <param name="connectionSting">Connection string.</param>
    public class NpgsqlConnectionFactory(string connectionSting) : IDbConnectionFactory
    {
        private readonly string _connectionString = connectionSting;

        /// <summary>
        /// Creates an instance of Postgres database connection and opens it.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>DB connection</returns>
        public async Task<IDbConnection> CreateConnectionAsync(CancellationToken token = default)
        {
            var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync(token);

            return connection;
        }
    }
}