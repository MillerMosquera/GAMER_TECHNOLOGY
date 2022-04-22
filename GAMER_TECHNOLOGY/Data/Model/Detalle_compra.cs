namespace GAMER_TECHNOLOGY.Data.Model
{
    public class Detalle_compra
    {
        public int id_detalle_compra { get; set; }
        public int id_orden_compra { get; set; }
        public int id_articulo { get; set; }
        public int cantidad { get; set; }
        public double valor_total { get; set; }
        public string email_user { get; set; }
    }
}
