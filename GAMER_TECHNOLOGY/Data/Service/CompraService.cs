using Dapper;
using GAMER_TECHNOLOGY.Data.Model;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GAMER_TECHNOLOGY.Data.Service
{

    public class CompraService : ICompraService
    {
        private readonly SqlConnectionConfiguration _configuration;

        public CompraService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Detalle_venta>> GetEmail(string email_user)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string SelectCompra = @"SELECT id_venta,id_articulo,cantidad,nombre,email_user,valor,sum(cantidad) as cantidad FROM dbo.detalleVenta d, dbo.AspNetUsers a WHERE d.email_user = a.Email and d.email_user = @email_user group by id_venta,id_articulo,cantidad,nombre,email_user,valor";
                var resultCompra = await conn.QueryAsync<Detalle_venta>(SelectCompra, new { email_user = email_user });
                return resultCompra.ToList();
            }
        }
        public async Task<IEnumerable<Detalle_venta>> GetId(int Id_Articulo)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string SelectCompra = @"SELECT id_venta,id_articulo,cantidad,nombre,email_user,valor,sum(cantidad) as cantidad FROM dbo.detalleVenta d WHERE Id_articulo = @Id_Articulo group by id_venta,id_articulo,cantidad,nombre,email_user,valor";
                var result = await conn.QueryAsync<Detalle_venta>(SelectCompra, new { Id_articulo = Id_Articulo });
                return result.ToList();
            }
        }

    }
}
