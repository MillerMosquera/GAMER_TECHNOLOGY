using GAMER_TECHNOLOGY.Data.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GAMER_TECHNOLOGY.Data.Service
{
    public interface ICartService
    {
        event Action OnChange;

        Task AddToCart(CartItem item);
        Task DeleteItem(CartItem item);
        Task EmptyCart();
        Task<List<CartItem>> GetCartItems();
    }
}