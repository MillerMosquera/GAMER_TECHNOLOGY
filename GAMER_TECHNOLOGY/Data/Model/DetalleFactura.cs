namespace GAMER_TECHNOLOGY.Data.Model
{
    public class DetalleFactura
    {
        public int numero_orden { get; set; }
        public int Codigo { get; set; }
        public int id_articulo { get; set; }
        public string Nombre { get; set; }
        public float Valor { get; set; }
        public float Cantidad { get; set; }
    }
}
