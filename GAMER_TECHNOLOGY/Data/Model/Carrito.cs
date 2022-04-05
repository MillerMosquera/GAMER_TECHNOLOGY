namespace GAMER_TECHNOLOGY.Data.Model
{
    public class Carrito
    {
        public int Id_articulo { get; set; }
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public string Categoria { get; set; }
        public double Precio { get; set; }
        public int Cantidad { get; set; }
        public string Email_user { get;  set; }

    }
}
