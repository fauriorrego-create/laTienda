namespace laTienda.DTOs
{
    public class ProductoCreateDTO
    {
        public int Idcategoria { get; set; }

        public string Nombreproducto { get; set; } = null!;

        public decimal Precioproducto { get; set; }

        public int Stockproducto { get; set; }

        public bool Estadoproducto { get; set; }
    }
}