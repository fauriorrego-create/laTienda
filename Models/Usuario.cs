using System.Collections.Generic;

namespace laTienda.Models;

public partial class Usuario
{
    public int Idusuario { get; set; }

    public string Tipodoc { get; set; } = null!;
    public string Nrodoc { get; set; } = null!;
    public string Nombre { get; set; } = null!;
    public string Email { get; set; } = null!;

    public byte[] Passwordhash { get; set; } = null!;
    public byte[] Passwordsalt { get; set; } = null!;

    // 🔥 IMPORTANTE: nullable safe
    public virtual ICollection<Usuariorole> Usuarioroles { get; set; }
        = new List<Usuariorole>();
}