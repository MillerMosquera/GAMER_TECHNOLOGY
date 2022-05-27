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
                parameters.Add("Email_user", checkout.email_user, DbType.String);
                parameters.Add("Nombre", checkout.nombre, DbType.String);
                parameters.Add("Apellido", checkout.apellido, DbType.String);
                parameters.Add("Direccion", checkout.direccion, DbType.String);
                parameters.Add("Apartamento", checkout.apartamento, DbType.String);
                parameters.Add("Departamento", checkout.departamento, DbType.String);
                parameters.Add("Ciudad", checkout.ciudad, DbType.String);

                const string InsertInfo = @"INSERT INTO dbo.Info_compra (Email_user, Nombre, Apellido, Direccion, Apartamento, Departamento, Ciudad) " +
                    "VALUES (@Email_user, @Nombre, @Apellido, @Direccion, @Apartamento, @Departamento, @Ciudad)";

                await conn.ExecuteAsync(InsertInfo, parameters);
            }
        }
        public async Task<IEnumerable<Checkout>> SelectCheckout(string email_user)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string Select = @"SELECT Email_user, Nombre, Apellido, Direccion, Apartamento, Departamento, Ciudad FROM dbo.Info_compra WHERE email_user = @email_user group by Email_user, Nombre, Apellido, Direccion, Apartamento, Departamento, Ciudad";
                return await conn.QueryAsync<Checkout>(Select, new { email_user = email_user });
                
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

    }
}
