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

        public async Task<IEnumerable<ResumenPago>> GetEmail(string email_user)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string SelectCompra = @"SELECT Id_articulo,nombre,descripcion,imagen,categoria,precio,sum(cantidad) as cantidad,codigo,email_user,descuento FROM dbo.Resumen_compra c, dbo.AspNetUsers a WHERE c.email_user = a.Email and c.email_user = @email_user group by Id_articulo,nombre,descripcion,imagen,categoria,precio,codigo,email_user, descuento";
                var resultCompra = await conn.QueryAsync<ResumenPago>(SelectCompra, new { email_user = email_user });
                return resultCompra.ToList();
            }
        }
        public async Task<ResumenPago> GetId(int Id_Articulo)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string SelectCompra = @"SELECT Id_articulo,nombre,descripcion,imagen,categoria,precio,sum(cantidad) as cantidad,codigo,email_user,descuento FROM dbo.Resumen_compra WHERE Id_articulo = @Id_Articulo group by Id_articulo,nombre,descripcion,imagen,categoria,precio,codigo,email_user, descuento";
                return await conn.QuerySingleAsync<ResumenPago>(SelectCompra, new { Id_articulo = Id_Articulo });
            }
        }

    }
}
