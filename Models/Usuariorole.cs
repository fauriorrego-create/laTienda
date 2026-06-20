namespace laTienda.Models;

public partial class Usuariorole
{
    public int Idusuariorol { get; set; }

    public int Idusuario { get; set; }

    public int Idrol { get; set; }

    // 🔥 ESTO ES LO QUE TE ESTABA ROMPIENDO EN RENDER
    public virtual Role? IdrolNavigation { get; set; }

    public virtual Usuario? IdusuarioNavigation { get; set; }
}