using GAMER_TECHNOLOGY.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GAMER_TECHNOLOGY.Data.Service
{
    public interface IVentasService
    {
        Task<IEnumerable<Venta>> GetEmail(string email_user);
        Task InsertDetalle_Venta(Detalle_venta detalle_venta);
        Task InsertVenta(Venta venta);
        Task<IEnumerable<Venta>> GetAll();
    }
}