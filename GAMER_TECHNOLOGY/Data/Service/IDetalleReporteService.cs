using GAMER_TECHNOLOGY.Data.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GAMER_TECHNOLOGY.Data.Service
{
    public interface IDetalleReporteService
    {
        Task<IEnumerable<Detalle_venta>> GetDetalleReporte();
        Task<IEnumerable<Detalle_venta>> GetAll();
        Task<IEnumerable<Detalle_venta>> GetDetalleReporteMensual(DateTime desde, DateTime hasta);
    }
}