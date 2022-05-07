using Dapper;
using GAMER_TECHNOLOGY.Data.Model;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GAMER_TECHNOLOGY.Data.Service
{
    public class CarritoService : ICarritoService
    {
        private readonly SqlConnectionConfiguration _configuration;

        public CarritoService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task InsertCarrito(Carrito carrito)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {

                var parameters = new DynamicParameters();
                parameters.Add("Id_articulo", carrito.Id_articulo, DbType.Int32);
                parameters.Add("Codigo", carrito.Codigo, DbType.Int32);
                parameters.Add("Nombre", carrito.Nombre, DbType.String);
                parameters.Add("Descripcion", carrito.Descripcion, DbType.String);
                parameters.Add("Imagen", carrito.Imagen, DbType.String);
                parameters.Add("Categoria", carrito.Categoria, DbType.String);
                parameters.Add("Precio", carrito.Precio, DbType.Double);
                parameters.Add("Cantidad", carrito.Cantidad, DbType.Int32);
                parameters.Add("Email_user",carrito.Email_user, DbType.String);


                const string InsertCarrito = @"INSERT INTO dbo.Carrito (Id_articulo, Codigo, Nombre, Descripcion, Imagen, Categoria, Precio, Cantidad, email_user) " +
                    "VALUES (@Id_articulo, @Codigo, @Nombre, @Descripcion,@Imagen,@Categoria,@Precio,@Cantidad,@email_user)";

                await conn.ExecuteAsync(InsertCarrito, new {carrito.Id_articulo,carrito.Codigo,carrito.Nombre,carrito.Descripcion,carrito.Imagen,carrito.Categoria,carrito.Precio,carrito.Cantidad,carrito.Email_user}, commandType: CommandType.Text);
            }
        }

        //Obtener todos los datos
        public async Task<IEnumerable<Carrito>> GetCart()
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string SelectCarrito = @"SELECT Id_articulo, Codigo, Nombre, Descripcion, Imagen, Categoria, Precio, Cantidad, email_user FROM dbo.Carrito";
                var resultCarrito = await conn.QueryAsync<Carrito>(SelectCarrito);
                return resultCarrito.ToList();
            }
        }
        //Obtener solo uno por el email
        public async Task<IEnumerable<Carrito>> GetEmail(string email_user)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string SelectCarrito = @"SELECT Id_articulo,nombre,descripcion,imagen,categoria,precio,sum(cantidad) as cantidad,codigo,email_user FROM dbo.Carrito c, dbo.AspNetUsers a WHERE c.email_user = a.Email and c.email_user = @email_user group by Id_articulo,nombre,descripcion,imagen,categoria,precio,codigo,email_user";
                var resultcart = await conn.QueryAsync<Carrito>(SelectCarrito, new {email_user = email_user});
                return resultcart.ToList();
            }
        }

        //actualizar
        public async Task UpdateArticulo(Carrito carrito)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("IdArticulo", carrito.Id_articulo, DbType.Int32);
                parameters.Add("Codigo", carrito.Codigo, DbType.Int32);
                parameters.Add("Nombre", carrito.Nombre, DbType.String);
                parameters.Add("Descripcion", carrito.Descripcion, DbType.String);
                parameters.Add("Imagen", carrito.Imagen, DbType.String);
                parameters.Add("Categoria", carrito.Categoria, DbType.String);
                parameters.Add("Precio", carrito.Precio, DbType.Double);
                parameters.Add("Cantidad", carrito.Cantidad, DbType.Int32);
                

                const string UpdateCart = @"UPDATE dbo.Carrito SET Id_articulo = @Id_articulo, Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, Imagen = @Imagen, Categoria = @Categoria, Precio = @Precio, Cantidad = @Cantidad" +
                "WHERE Id_articulo = @Id_articulo";

                await conn.ExecuteAsync(UpdateCart, new { carrito.Id_articulo, carrito.Codigo, carrito.Nombre, carrito.Descripcion, carrito.Imagen, carrito.Categoria, carrito.Precio, carrito.Cantidad });
            }
        }

        public async Task Delete(Carrito cart)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string DeleteCart = @"DELETE FROM dbo.Carrito WHERE Id_articulo = @Id_articulo";

                await conn.ExecuteAsync(DeleteCart, new { cart.Id_articulo});
            }
        }
    }
}
