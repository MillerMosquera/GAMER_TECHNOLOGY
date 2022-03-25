using GAMER_TECHNOLOGY.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GAMER_TECHNOLOGY.Data.Service
{
    public interface ICarritoService
    {
        Task Delete(int id);
        Task<IEnumerable<Carrito>> GetAll();
        Task<IEnumerable<Carrito>> GetId(int id);
        Task InsertCarrito(Carrito carrito);
        Task UpdateArticulo(Carrito carrito);
    }
}