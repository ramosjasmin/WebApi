using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public static class RolEndpoint
{
    public static RouteGroupBuilder MapRolEndpoints(this RouteGroupBuilder app)
    {

        app.MapPost("/rol", ([FromBody] Rol rol, EscuelaContext context)=>{
            if(rol.Nombre != null && rol.Nombre != string.Empty)
            {
                context.Rols.Add(rol);
                context.SaveChanges();
                return Results.Created();
            }
            else
            {
                return Results.BadRequest();
            }

        });
            
        app.MapGet("/roles", (EscuelaContext context)=>{
            return Results.Ok(context.Rols.ToList());
        });
        
        app.MapGet("/rol", ([FromQuery] Guid idRol, EscuelaContext context)=>{
            var rolAEspecifico = context.Rols.FirstOrDefault(rol => rol.Id == idRol);
            if (rolAEspecifico != null)
            {
                return Results.Ok(rolAEspecifico); 
            }
            else
            {
                return Results.NotFound(); 
            }
        });

        app.MapPut("/rol", ([FromQuery] Guid idRol, [FromBody] Rol rol, EscuelaContext context)=>{
            
            var rolAActualizar = context.Rols.FirstOrDefault(rol => rol.Id == idRol);
            if(rolAActualizar == null)
                return Results.NotFound();
            if(rolAActualizar.Nombre != rol.Nombre)
                return Results.BadRequest();

            rolAActualizar.Habilitado=rol.Habilitado;
            context.SaveChanges();
            return Results.Ok(context.Rols);
        });

        app.MapDelete("/rol", ([FromQuery] Guid idRol, EscuelaContext context)=>{
            var rolAEliminar = context.Rols.FirstOrDefault(rol => rol.Id == idRol);
            if (rolAEliminar != null)
            {
                context.Rols.Remove(rolAEliminar);
                context.SaveChanges();
                return Results.NoContent(); 
            }
            else
            {
                return Results.NotFound(); 
            }
        });

        app.MapPost("/rol/{idRol}/usuario/{idUsuario}", (Guid idRol, int idUsuario, EscuelaContext context)=>{
            var rol = context.Rols.FirstOrDefault(rol => rol.Id == idRol);
            var usuario = context.Usuarios.FirstOrDefault(usuario => usuario.Id == idUsuario);

            if (usuario != null && rol != null)
            {
                context.Usuariorols.Add(new Usuariorol{Id=0, IdrolNavigation=rol, IdusuarioNavigation=usuario});
                context.SaveChanges();
                return Results.Ok();
            }

            return Results.NotFound();
        });

        app.MapDelete("/rol/{idRol}/usuario/{idUsuario}", (Guid idRol, int idUsuario, EscuelaContext context)=>{
            
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