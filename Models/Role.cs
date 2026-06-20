using System.Collections.Generic;

namespace laTienda.Models;

public partial class Role
{
    public int Idrol { get; set; }

    public string Nombrerol { get; set; } = null!;

    // 🔥 nullable safe
    public virtual ICollection<Usuariorole> Usuarioroles { get; set; }
        = new List<Usuariorole>();
}