using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Usuariorol
{
    public int Id { get; set; }

    public Guid Idrol { get; set; }

    public int Idusuario { get; set; }

    public virtual Rol IdrolNavigation { get; set; } = null!;

    public virtual Usuario IdusuarioNavigation { get; set; }= null!;
}
