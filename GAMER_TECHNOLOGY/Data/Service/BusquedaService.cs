using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using GAMER_TECHNOLOGY.Data.Model;
using Microsoft.Data.SqlClient;

namespace GAMER_TECHNOLOGY.Data.Service
{
    public class BusquedaService : IBusquedaService
    {
        private readonly SqlConnectionConfiguration _configuration;

        public BusquedaService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IEnumerable<Articulo>> GetBusqueda(string busqueda)
        {
            using (var connection = new SqlConnection(_configuration.Value))
            {
                const string sql = @"SELECT * FROM dbo.Articulo WHERE Nombre LIKE '%' + @Nombre + '%' OR Categoria LIKE '%' + @Categoria + '%' ";
                var result = await connection.QueryAsync<Articulo>(sql, new { nombre = @busqueda, categoria = @busqueda });
                return result.ToList();
            }
        }


    }
}
