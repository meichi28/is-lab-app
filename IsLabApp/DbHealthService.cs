using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace NotesApi.Services
{
    public class DbHealthService
    {
        private readonly IConfiguration _configuration;

        public DbHealthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<(bool Success, string Message)> CheckConnectionAsync()
        {
            var connectionString = _configuration.GetConnectionString("Mssql");

            try
            {
                using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();
                return (true, "ok");
            }
            catch (Exception ex)
            {
                return (false, $"Ошибка подключения: {ex.Message}");
            }
        }
    }
}