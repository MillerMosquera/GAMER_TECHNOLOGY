using GAMER_TECHNOLOGY.Data.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GAMER_TECHNOLOGY.Data.Service
{
    public interface ICartService
    {
        Task AddToCart(Carrito carrito);
        Task DeleteItem(Carrito carrito);
        Task EmptyCart();
        Task<List<Carrito>> GetCartItems();
    }
}