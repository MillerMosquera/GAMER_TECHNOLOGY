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
        public async Task<IEnumerable<Detalle_venta>> GetDetalleFactura(int id_articulo)
        {
            
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string query = @"SELECT id_venta,nombre,valor,fecha,sum(cantidad) as cantidad
                                        FROM dbo.detalleVenta
                                        WHERE id_articulo = @id_articulo group by id_venta,nombre,valor,fecha";
               var detalle = await conn.QueryAsync<Detalle_venta>(query, new { id_articulo = @id_articulo }, commandType: CommandType.Text);
               return detalle.ToList();
            }

        }
        public async Task<IEnumerable<Checkout>> GetDetalleUser(string email_user)
        {

            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string query = @"SELECT Email_user,Nombre,Apellido,Direccion,Ciudad
                                        FROM dbo.Info_compra
                                        WHERE Email_user = @email_user group by Email_user,Nombre,Apellido,Direccion,Ciudad";
                var detalle = await conn.QueryAsync<Checkout>(query, new { Email_user = @email_user }, commandType: CommandType.Text);
                return detalle.ToList();
            }

        }

    }
}
