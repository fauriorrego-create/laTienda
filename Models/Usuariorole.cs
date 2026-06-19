using System;
using System.Collections.Generic;

namespace laTienda.Models;

public partial class Usuariorole
{
    public int Idusuariorol { get; set; }

    public int Idusuario { get; set; }

    public int Idrol { get; set; }

    public virtual Role IdrolNavigation { get; set; } = null!;

    public virtual Usuario IdusuarioNavigation { get; set; } = null!;
}
