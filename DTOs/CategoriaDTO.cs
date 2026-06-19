namespace laTienda.DTOs
{
    public class CategoriaDTO
    {
        public int Idcategoria { get; set; }

        public string Nombrecategoria { get; set; } = null!;

        public bool? Estadocategoria { get; set; }
    }
}