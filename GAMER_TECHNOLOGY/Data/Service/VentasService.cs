using Dapper;
using GAMER_TECHNOLOGY.Data.Model;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GAMER_TECHNOLOGY.Data.Service
{
    public class VentasService : IVentasService
    {
        private readonly SqlConnectionConfiguration _configuration;

        public VentasService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Venta>> GetEmail(string email_user)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string SelectVenta = @"SELECT  v.id_venta,v.email_user,v.fecha,id_articulo,a.nombre,a.descripcion,a.imagen,a.categoria,a.precio,sum(a.cantidad) as a.cantidad,a.codigo,a.email_user FROM dbo.Venta v,dbo.detalleVenta d,dbo.Articulos a WHERE v.email_user = d.Email and v.email_user = @email_user and d.id_articulo = a.Id_articulo group by  v.id_venta,v.email_user,v.fecha,id_articulo,a.nombre,a.descripcion,a.imagen,a.categoria,a.precio,a.codigo,a.email_user";
                var resultcart = await conn.QueryAsync<Venta>(SelectVenta, new { email_user = email_user });
                return resultcart.ToList();
            }
        }
        public async Task InsertVenta(Venta venta)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {

                var parameters = new DynamicParameters();
                parameters.Add("id_venta", venta.id_venta, DbType.Int32);
                parameters.Add("email_user", venta.email_user, DbType.String);
                parameters.Add("fecha", venta.fecha, DbType.DateTime);

                const string InsertVenta = @"INSERT INTO dbo.Venta (id_venta, email_user, fecha)
                   VALUES (@id_venta, @email_user, @fecha)";

                await conn.ExecuteAsync(InsertVenta, parameters);
            }
        }
        public async Task InsertDetalle_Venta(Detalle_venta detalle_venta)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {

                var parameters = new DynamicParameters();
                parameters.Add("id_venta", detalle_venta.id_venta, DbType.Int32);
                parameters.Add("id_articulo", detalle_venta.id_articulo, DbType.Int32);
                parameters.Add("cantidad", detalle_venta.cantidad, DbType.Int32);
                parameters.Add("email_user", detalle_venta.email_user, DbType.String);
                parameters.Add("nombre",detalle_venta.nombre,DbType.String);
                parameters.Add("valor", detalle_venta.valor, DbType.Double);


                const string InsertDetalle = @"INSERT INTO dbo.detalleVenta SELECT v.id_venta,c.id_articulo, c.cantidad, v.email_user, c.nombre, @valor,v.fecha FROM dbo.Venta v,dbo.Carrito c WHERE v.email_user = c.email_user";

                await conn.ExecuteAsync(InsertDetalle, parameters);
            }
        }
        public async Task<IEnumerable<Venta>> GetAll()
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string SelectVenta = @"SELECT * FROM dbo.Venta";
                return await conn.QueryAsync<Venta>(SelectVenta);
                
            }
        }

    }
}
