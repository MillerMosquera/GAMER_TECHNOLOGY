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
                parameters.Add("Idcart", carrito.Idcart, DbType.Int32);
                parameters.Add("IdArticulo", carrito.IdArticulo, DbType.Int32);
                parameters.Add("Codigo", carrito.Codigo, DbType.Int32);
                parameters.Add("Nombre", carrito.Nombre, DbType.String);
                parameters.Add("Descripcion", carrito.Descripcion, DbType.String);
                parameters.Add("Imagen", carrito.Imagen, DbType.String);
                parameters.Add("Categoria", carrito.Categoria, DbType.String);
                parameters.Add("Precio", carrito.Precio, DbType.Decimal);
                parameters.Add("Cantidad", carrito.Cantidad, DbType.Int32);
                parameters.Add("Subtotal", carrito.Subtotal, DbType.Decimal);

                const string InsertCarrito = @"INSERT INTO dbo.Carrito (IdCart,IdArticulo, Codigo, Nombre, Descripcion, Imagen, Categoria, Precio, Cantidad, Subtotal) " +
                    "VALUES (@IdCart,@IdArticulo, @Codigo, @Nombre, @Descripcion,@Imagen,@Categoria,@Precio,@Cantidad,@Subtotal)";

                await conn.ExecuteAsync(InsertCarrito, parameters);
            }
        }

        //Obtener todos los datos
        public async Task<IEnumerable<Carrito>> GetAll()
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string SelectCarrito = @"SELECT * FROM dbo.Carrito";
                var resultCarrito = await conn.QueryAsync<Carrito>(SelectCarrito);
                return resultCarrito.ToList();
            }
        }
        //Obtener solo uno por el id
        public async Task<IEnumerable<Carrito>> GetId(int id)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string SelectCarrito = @"SELECT * FROM dbo.Carrito WHERE IdCart = @IdCart";
                return (IEnumerable<Carrito>)await conn.QuerySingleAsync<Carrito>(SelectCarrito, new { IdCart = id });
            }
        }

        //actualizar
        public async Task UpdateArticulo(Carrito carrito)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("Idcart", carrito.Idcart, DbType.Int32);
                parameters.Add("IdArticulo", carrito.IdArticulo, DbType.Int32);
                parameters.Add("Codigo", carrito.Codigo, DbType.Int32);
                parameters.Add("Nombre", carrito.Nombre, DbType.String);
                parameters.Add("Descripcion", carrito.Descripcion, DbType.String);
                parameters.Add("Imagen", carrito.Imagen, DbType.String);
                parameters.Add("Categoria", carrito.Categoria, DbType.String);
                parameters.Add("Precio", carrito.Precio, DbType.Decimal);
                parameters.Add("Cantidad", carrito.Cantidad, DbType.Int32);
                parameters.Add("Subtotal", carrito.Subtotal, DbType.Decimal);

                const string UpdateCart = @"UPDATE dbo.Carrito SET IdCart = @IdCart, IdArticulo = @IdArticulo, Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, Imagen = @Imagen, Categoria = @Categoria, Precio = @Precio, Cantidad = @Cantidad, Subtotal = @Subtotal " +
                "WHERE IdCart = @IdCart";

                await conn.ExecuteAsync(UpdateCart, new { carrito.Idcart, carrito.IdArticulo, carrito.Codigo, carrito.Nombre, carrito.Descripcion, carrito.Imagen, carrito.Categoria, carrito.Precio, carrito.Cantidad, carrito.Subtotal });
            }
        }

        public async Task Delete(int id)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string DeleteCart = @"DELETE FROM dbo.Carrito WHERE IdCart = @IdCart";

                await conn.ExecuteAsync(DeleteCart, new { IdCart = id });
            }
        }


    }
}
