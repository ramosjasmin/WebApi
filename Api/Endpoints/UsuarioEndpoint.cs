using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public static class UsuarioEndpoint
{
    public static RouteGroupBuilder MapUsuarioEndpoints(this RouteGroupBuilder app)
    {

        app.MapPost("/usuario", ([FromBody] Usuario usuario, EscuelaContext context)=>
        {
            if(usuario.Nombre != null && usuario.Nombre != string.Empty && usuario.Email !=null && usuario.Email !=string.Empty && usuario.Contrasenia != null && usuario.Contrasenia != string.Empty && usuario.Nombreusuario != null && usuario.Nombreusuario != string.Empty)
            {
                context.Usuarios.Add(usuario);
                context.SaveChanges();
                return Results.Created(); // Codigo 201
            }
            else
            {
                return Results.BadRequest(); // Codigo 400
            }

        });

        app.MapGet("/usuarios", (EscuelaContext context) =>{
            var filtrado=context.Usuarios.ToList();
            
            return Results.Ok(filtrado.Where(word => word.Habilitido == true).ToList());
        });

        //Obtener usuario por id
        app.MapGet("/usuario", ([FromQuery] int idUsuario, EscuelaContext context) =>
        {
            var usuarioAEspecifico = context.Usuarios.FirstOrDefault(usuario => usuario.Id == idUsuario);
            if (usuarioAEspecifico != null)
            {
                return Results.Ok(usuarioAEspecifico); //Codigo 200
            }
            else
            {
                return Results.NotFound(); //Codigo 404
            }
        });

        app.MapPut("/usuario", ([FromQuery] int idUsuario, [FromBody] Usuario usuario, EscuelaContext context) =>
        {
            var usuarioAActualizar = context.Usuarios.FirstOrDefault(usuario => usuario.Id == idUsuario);
            if(usuarioAActualizar == null)
                return Results.NotFound();        
            if(usuarioAActualizar.Nombre != usuario.Nombre)
                return Results.BadRequest();

            usuarioAActualizar.Contrasenia = usuario.Contrasenia;
            usuarioAActualizar.Email= usuario.Email;
            usuarioAActualizar.Nombreusuario= usuario.Nombreusuario;
            context.SaveChanges();
            return Results.Ok(context.Usuarios);

        });

        app.MapDelete("/usuario", ([FromQuery] int idUsuario, EscuelaContext context) =>
        {
            var usuarioAEliminar = context.Usuarios.FirstOrDefault(usuario => usuario.Id == idUsuario);
            if (usuarioAEliminar != null)
            {
                //context.Usuarios.Remove(usuarioAEliminar);
                usuarioAEliminar.Habilitido = false;
                context.SaveChanges();  
                return Results.NoContent(); //Codigo 204
            }
            else
            {
                return Results.NotFound(); //Codigo 404
            }
        });
        // usuario a un rol
        app.MapPost("/usuario/{idUsuario}/rol/{idRol}", (int idUsuario, Guid idRol, EscuelaContext context)=>{
            var rol = context.Rols.FirstOrDefault(rol => rol.Id == idRol);
            var usuario = context.Usuarios.FirstOrDefault(usuario => usuario.Id == idUsuario);  
            if (usuario != null && rol != null)
            {
                context.Usuariorols.Add(new Usuariorol{Id =0, IdrolNavigation= rol, IdusuarioNavigation= usuario});
                context.SaveChanges();
                return Results.Ok();
            }

            return Results.NotFound();
        });

        app.MapDelete("/usuario/{idUsuario}/rol/{idRol}", (int idUsuario, Guid idRol, EscuelaContext context)=>{
            var usuarioRol= context.Usuariorols.FirstOrDefault(x=> x.Idusuario == idUsuario && x.Idrol == idRol);
            if (usuarioRol is not null)
            {
                context.Usuariorols.Remove(usuarioRol);
                context.SaveChanges();
                return Results.Ok();
            }

            return Results.NotFound();
        });
        return app;
    }
}