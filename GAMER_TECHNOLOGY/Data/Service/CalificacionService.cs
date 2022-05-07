using Dapper;
using GAMER_TECHNOLOGY.Data.Model;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;

namespace GAMER_TECHNOLOGY.Data.Service
{
    public class CalificacionService : ICalificacionService
    {
        private readonly SqlConnectionConfiguration _configuration;

        public CalificacionService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task InsertCalif(Calificacion calificacion)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {

                var parameters = new DynamicParameters();
                parameters.Add("id_articulo", calificacion.id_articulo, DbType.Int32);
                parameters.Add("nombre", calificacion.nombre, DbType.String);
                parameters.Add("comentario", calificacion.comentario, DbType.String);

                const string InsertCalificacion = @"INSERT INTO dbo.Calificacion (id_articulo,nombre, comentario) " +
                    "VALUES (@id_articulo, @nombre, @comentario)";

                await conn.ExecuteAsync(InsertCalificacion, parameters);
            }
        }
    }
}
