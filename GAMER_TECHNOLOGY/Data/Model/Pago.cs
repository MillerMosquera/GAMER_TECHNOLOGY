namespace GAMER_TECHNOLOGY.Data.Model
{
    public class Pago
    {
        public int id_pago { get; set; }
        public string nombre_tarjeta { get; set; }
        public int numero_tarjeta { get; set; }
        public int cvv { get; set; }
        public int mes_pago { get; set; }
        public int año_pago { get; set; }
        public double valor_pago { get; set; }
        public int numero_orden { get; set; }
    }
}
