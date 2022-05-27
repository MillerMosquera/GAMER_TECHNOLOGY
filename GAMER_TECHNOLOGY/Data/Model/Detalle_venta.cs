using System;

namespace GAMER_TECHNOLOGY.Data.Model
{
    public class Detalle_venta
    {
        public int id_venta { get; set; }
        public int id_articulo { get; set; }
        public int cantidad { get; set; }
        public string email_user { get; set; }
        public string nombre { get; set; }
        public double valor { get; set; }
        public DateTime fecha { get; set; }
    }
}
