using Dapper;
using GAMER_TECHNOLOGY.Data.Model;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GAMER_TECHNOLOGY.Data.Service
{
    public class CheckoutService : ICheckoutService
    {
        private readonly SqlConnectionConfiguration _configuration;

        public CheckoutService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task InsertCheckout(Checkout checkout)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {

                var parameters = new DynamicParameters();
                parameters.Add("Email_user", checkout.email, DbType.String);
                parameters.Add("Numero_celular", checkout.numero_celular, DbType.Int32);
                parameters.Add("Nombre", checkout.nombre, DbType.String);
                parameters.Add("Apellido", checkout.apellido, DbType.String);
                parameters.Add("Direccion", checkout.direccion, DbType.String);
                parameters.Add("Apartamento", checkout.apartamento, DbType.String);
                parameters.Add("Departamento", checkout.departamento, DbType.String);
                parameters.Add("Ciudad", checkout.ciudad, DbType.String);

                const string InsertInfo = @"INSERT INTO dbo.Info_compra (Email_user, Numero_celular, Nombre, Apellido, Direccion, Apartamento, Departamento, Ciudad) " +
                    "VALUES (@Email_user, @Numero_celular, @Nombre, @Apellido, @Direccion, @Apartamento, @Departamento, @Ciudad)";

                await conn.ExecuteAsync(InsertInfo, parameters);
            }
        }
        public async Task<Checkout> SelectCheckout(string email_user)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string Select = @"SELECT * FROM dbo.Info_compra WHERE email_user = @email_user";
                return await conn.QuerySingleAsync<Checkout>(Select, new { email_user = email_user });
                
            }
        }
        public async Task InsertCarrito(ResumenPago resumen)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {

                var parameters = new DynamicParameters();
                parameters.Add("id_articulo", resumen.Id_articulo, DbType.Int32);
                parameters.Add("nombre", resumen.Nombre, DbType.String);
                parameters.Add("descripcion", resumen.Descripcion, DbType.String);
                parameters.Add("imagen", resumen.Imagen, DbType.String);
                parameters.Add("codigo", resumen.Codigo, DbType.Int32);
                parameters.Add("precio", resumen.Precio, DbType.Double);
                parameters.Add("cantidad", resumen.Cantidad, DbType.Int32);
                parameters.Add("descuento", resumen.Descuento, DbType.Double);
                parameters.Add("categoria", resumen.Categoria, DbType.String);
                parameters.Add("email_user", resumen.Email_user, DbType.String);


                const string InsertResumen = @"INSERT INTO dbo.Resumen_compra SELECT * FROM dbo.Carrito";

                await conn.ExecuteAsync(InsertResumen, parameters);
            }
        }
        public async Task InsertPago(Pago pago)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {

                var parameters = new DynamicParameters();
                parameters.Add("id_pago", pago.id_pago, DbType.Int32);
                parameters.Add("nombre_tarjeta", pago.nombre_tarjeta, DbType.String);
                parameters.Add("numero_tarjeta", pago.numero_tarjeta, DbType.Int32);
                parameters.Add("cvv", pago.cvv, DbType.Int32);
                parameters.Add("mes_pago", pago.mes_pago, DbType.Int32);
                parameters.Add("año_pago", pago.año_pago, DbType.Double);
                parameters.Add("valor_pago", pago.valor_pago, DbType.Int32);
                parameters.Add("numero_orden", pago.numero_orden, DbType.Int32);

                const string InsertPago = @"INSERT INTO dbo.pago (nombre_tarjeta, numero_tarjeta, cvv, mes_pago,año_pago,valor_pago,numero_orden)
                   VALUES (@nombre_tarjeta,@numero_tarjeta,@cvv,@mes_pago,@año_pago,@valor_pago,@numero_orden)";

                await conn.ExecuteAsync(InsertPago, parameters);
            }
        }
        public async Task InsertDetalle_Compra(Detalle_compra detalle)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {

                var parameters = new DynamicParameters();
                parameters.Add("id_detalle_compra", detalle.id_detalle_compra, DbType.Int32);
                parameters.Add("id_orden_compra", detalle.id_orden_compra, DbType.Int32);
                parameters.Add("id_producto", detalle.id_articulo, DbType.Int32);
                parameters.Add("cantidad", detalle.cantidad, DbType.Int32);
                parameters.Add("email_user", detalle.email_user, DbType.String);


                const string InsertResumen = @"INSERT INTO dbo.Detalle_compra SELECT a.Idarticulo, o.id_orden_compra, c.cantidad, c.email_user FROM dbo.Articulo a, dbo.Orden_compra o, dbo.Carrito c";

                await conn.ExecuteAsync(InsertResumen, parameters);
            }
        }

    }
}
