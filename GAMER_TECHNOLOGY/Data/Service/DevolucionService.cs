using Dapper;
using GAMER_TECHNOLOGY.Data.Model;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;

namespace GAMER_TECHNOLOGY.Data.Service
{
    public class DevolucionService : IDevolucionService
    {
        private readonly SqlConnectionConfiguration _configuration;

        public DevolucionService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task InsertDevolucion(Devolucion devolucion)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {

                var parameters = new DynamicParameters();
                parameters.Add("id_articulo", devolucion.id_articulo, DbType.Int32);
                parameters.Add("motivo", devolucion.motivo, DbType.String);
                parameters.Add("email_user", devolucion.email_user, DbType.String);

                const string Insert = @"INSERT INTO dbo.Devolucion (id_articulo,motivo, email_user) " +
                    "VALUES (@id_articulo, @motivo, @email_user)";

                await conn.ExecuteAsync(Insert, parameters);
            }
        }
    }
}
