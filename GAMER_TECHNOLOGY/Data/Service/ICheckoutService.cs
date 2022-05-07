using GAMER_TECHNOLOGY.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GAMER_TECHNOLOGY.Data.Service
{
    public interface ICheckoutService
    {
        Task InsertCarrito(ResumenPago resumen);
        Task InsertCheckout(Checkout checkout);
        Task InsertDetalle_Compra(Detalle_compra detalle);
        Task InsertPago(Pago pago);
        Task<Checkout> SelectCheckout(string email_user);
    }
}