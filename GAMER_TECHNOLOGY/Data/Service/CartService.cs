using GAMER_TECHNOLOGY.Data.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Blazored.Toast.Services;



namespace GAMER_TECHNOLOGY.Data.Service
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IToastService _toastService;
        private readonly IArticuloService _articuloService;

    

        public CartService(ILocalStorageService localStorage, IArticuloService articuloService, IToastService toastService)
        {
            _localStorage = localStorage;
            _toastService = toastService;
            _articuloService = articuloService;
        }

        public async Task AddToCart(Carrito carrito)
        {
            var cart = await _localStorage.GetItemAsync<List<Carrito>>("cart");
            if (cart == null)
            {
                cart = new List<Carrito>();
            }

            var sameItem = cart
                .Find(x => x.Id_articulo == carrito.Id_articulo && x.Codigo == carrito.Codigo);
            if (sameItem == null)
            {
                cart.Add(carrito);
            }
            else
            {
                sameItem.Cantidad += carrito.Cantidad;
            }

            await _localStorage.SetItemAsync("cart", cart);

            var articulo = await _articuloService.GetId(carrito.Id_articulo);
            _toastService.ShowSuccess("Added to cart:");

         
        }
        public async Task<List<Carrito>> GetCartItems()
        {
            var cart = await _localStorage.GetItemAsync<List<Carrito>>("cart");
            if (cart == null)
            {
                return new List<Carrito>();
            }
            return cart;
        }

        public async Task DeleteItem(Carrito carrito)
        {
            var cart = await _localStorage.GetItemAsync<List<Carrito>>("cart");
            if (cart == null)
            {
                return;
            }

            var cartItem = cart.Find(x => x.Id_articulo == carrito.Id_articulo && x.Codigo == carrito.Codigo);
            cart.Remove(cartItem);

            await _localStorage.SetItemAsync("cart", cart);
            
        }

        public async Task EmptyCart()
        {
            await _localStorage.RemoveItemAsync("cart");
            
        }
    }
}
