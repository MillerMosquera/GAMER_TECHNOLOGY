using Dapper;
using GAMER_TECHNOLOGY.Data.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GAMER_TECHNOLOGY.Data.Service
{
    public class DetalleReporteService : IDetalleReporteService
    {
        private readonly SqlConnectionConfiguration _configuration;

        public DetalleReporteService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IEnumerable<Detalle_venta>> GetDetalleReporte()
        {

            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string query = @"SELECT id_articulo,nombre,valor,sum(cantidad) as cantidad FROM dbo.detalleVenta group by id_articulo,nombre,valor order by sum(cantidad) DESC";
                var detalle = await conn.QueryAsync<Detalle_venta>(query);
                return detalle.ToList();
            }

        }
        public async Task<IEnumerable<Detalle_venta>> GetDetalleReporteMensual(DateTime desde,DateTime hasta)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string query = @"SELECT id_venta,id_articulo,nombre,valor,fecha, sum(cantidad) as cantidad FROM dbo.detalleVenta WHERE fecha BETWEEN @desde and @hasta group by id_venta,id_articulo,nombre,valor,fecha order by sum(cantidad) ";
                var result = await conn.QueryAsync<Detalle_venta>(query, new {desde = @desde,hasta = @hasta});
                return result.ToList();
            }
        }
        public async Task<IEnumerable<Detalle_venta>> GetAll()
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                const string SelectDetalle = @"SELECT * FROM dbo.detalleVenta";
                var result = await conn.QueryAsync<Detalle_venta>(SelectDetalle);
                return result.ToList();
            }
        }
    }
}
