using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Nombreusuario { get; set; }

    public string? Contrasenia { get; set; }

    public string? Email { get; set; }

    public bool? Habilitido { get; set; }

    public DateTime? Fechacreacion { get; set; }

}
