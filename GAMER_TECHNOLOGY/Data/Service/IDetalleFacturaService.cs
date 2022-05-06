using GAMER_TECHNOLOGY.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GAMER_TECHNOLOGY.Data.Service
{
    public interface IDetalleFacturaService
    {
        Task<IEnumerable<DetalleFactura>> GetDetalleFactura(int id);
    }
}