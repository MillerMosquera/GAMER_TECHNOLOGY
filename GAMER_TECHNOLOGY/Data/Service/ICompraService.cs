using GAMER_TECHNOLOGY.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GAMER_TECHNOLOGY.Data.Service
{
    public interface ICompraService
    {
        Task<IEnumerable<ResumenPago>> GetEmail(string email_user);
        Task<ResumenPago> GetId(int Id_Articulo);
    }
}