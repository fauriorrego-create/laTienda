using System;
using System.Collections.Generic;

namespace laTienda.Models;

public partial class Categoria
{
    public int Idcategoria { get; set; }

    public string Nombrecategoria { get; set; } = null!;

    public bool? Estadocategoria { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
