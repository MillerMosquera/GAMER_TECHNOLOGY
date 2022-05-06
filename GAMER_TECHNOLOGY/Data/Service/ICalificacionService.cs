using GAMER_TECHNOLOGY.Data.Model;
using System.Threading.Tasks;

namespace GAMER_TECHNOLOGY.Data.Service
{
    public interface ICalificacionService
    {
        Task InsertCalif(Calificacion calificacion);
    }
}