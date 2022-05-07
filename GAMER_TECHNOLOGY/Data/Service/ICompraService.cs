using GAMER_TECHNOLOGY.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GAMER_TECHNOLOGY.Data.Service
{
    public interface ICompraService
    {
        Task<IEnumerable<Detalle_venta>> GetEmail(string email_user);
        Task<IEnumerable<Detalle_venta>> GetId(int Id_Articulo);
    }
}