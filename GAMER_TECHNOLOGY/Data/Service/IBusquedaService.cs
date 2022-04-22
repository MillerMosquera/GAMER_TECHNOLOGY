using System.Collections.Generic;
using System.Threading.Tasks;
using GAMER_TECHNOLOGY.Data.Model;

namespace GAMER_TECHNOLOGY.Data.Service
{
    public interface IBusquedaService
    {
        Task<IEnumerable<Articulo>> GetBusqueda(string busqueda);
    }
}