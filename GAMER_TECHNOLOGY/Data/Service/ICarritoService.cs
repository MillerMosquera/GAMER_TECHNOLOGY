using GAMER_TECHNOLOGY.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GAMER_TECHNOLOGY.Data.Service
{
    public interface ICarritoService
    {
        Task Delete(Carrito cart);
        Task<IEnumerable<Carrito>> GetCart();
        Task<IEnumerable<Carrito>> GetEmail(string email_user);
        Task InsertCarrito(Carrito carrito);
        Task UpdateArticulo(Carrito carrito);
    }
}