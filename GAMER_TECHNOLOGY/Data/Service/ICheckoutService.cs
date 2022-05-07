using GAMER_TECHNOLOGY.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GAMER_TECHNOLOGY.Data.Service
{
    public interface ICheckoutService
    {
        Task InsertCheckout(Checkout checkout);
        Task InsertPago(Pago pago);
        Task<IEnumerable<Checkout>> SelectCheckout(string email_user);


    }
}