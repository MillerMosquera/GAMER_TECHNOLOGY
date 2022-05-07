using GAMER_TECHNOLOGY.Data.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GAMER_TECHNOLOGY.Data.PDF
{
    public interface IReportePDF
    {
        FileResult descargarPDF();
        Task GenerarReporte(IEnumerable<Venta> venta, IEnumerable<Detalle_venta> detalleVenta);
    }
}