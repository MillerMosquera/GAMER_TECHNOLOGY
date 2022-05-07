using GAMER_TECHNOLOGY.Data.Model;
using System.Threading.Tasks;

namespace GAMER_TECHNOLOGY.Data.Service
{
    public interface IDevolucionService
    {
        Task InsertDevolucion(Devolucion devolucion);
    }
}