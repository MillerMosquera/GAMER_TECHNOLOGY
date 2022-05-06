using Dapper;
using GAMER_TECHNOLOGY.Data.Model;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GAMER_TECHNOLOGY.Data.Service
{
    public class DetalleFacturaService : IDetalleFacturaService
    {
        private readonly SqlConnectionConfiguration _configuration;

        public DetalleFacturaService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IEnumerable<DetalleFactura>> GetDetalleFactura(int id_articulo)
        {
            
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string query = @"SELECT p.numero_orden, r.id_articulo, r.nombre, r.precio, r.cantidad
                                        FROM dbo.Resumen_compra r, dbo.pago p
                                        WHERE r.Id_articulo=@id_articulo group by p.numero_orden,r.id_articulo,r.nombre,r.precio, r.cantidad";
               var detalle = await conn.QueryAsync<DetalleFactura>(query, new { id_articulo = id_articulo }, commandType: CommandType.Text);
               return detalle.ToList();
            }

        }

    }
}
