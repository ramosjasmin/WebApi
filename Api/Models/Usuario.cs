using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Nombreusuario { get; set; }= null!;

    public string Contrasenia { get; set; }= null!;

    public string Email { get; set; }= null!;

    public bool Habilitido { get; set; }

    public DateTime? Fechacreacion { get; set; }

}
