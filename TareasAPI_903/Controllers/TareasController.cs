using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;
using TareasAPI_903.Models;

namespace TareasAPI_903.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        //Variable de ccontexto de BD
        private readonly Bdtareas903Context _baseDatos;

        public TareasController(Bdtareas903Context baseDatos)
        {
            this._baseDatos = baseDatos;
        }

        //Método GET ListarTareas
        [HttpGet]
        [Route("ListaTareas")]
        public async Task<IActionResult> Lista()
        {
            var listaTareas = await
                _baseDatos.Tareas.ToListAsync();
            return Ok(listaTareas);
        }

        //Método POST AgregarTarea
        [HttpPost]
        [Route("AgregarTarea")]
        public async Task<IActionResult> Agregar([FromBody] Tarea request)
        {
            await _baseDatos.Tareas.AddAsync(request);
            await _baseDatos.SaveChangesAsync();
            return Ok(request);
        }

        //Método PUT ModificarTarea
        [HttpPut]
        [Route("ModificarTarea/{id}")]
        public async Task<IActionResult> Modificar(int id, [FromBody] Tarea request)
        {
            var tareaModificar = await
                _baseDatos.Tareas.FindAsync(id);

            if (tareaModificar == null)
            {
                return BadRequest("La tarea no existe");
            }

            tareaModificar.Nombre = request.Nombre;
            try
            {
                await _baseDatos.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return NotFound();
            }

            return Ok();
        }

        //Método DELETE EliminarTarea
        [HttpDelete]
        [Route("EliminarTarea/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var tareaEliminar = await
                _baseDatos.Tareas.FindAsync(id);

            if (tareaEliminar == null)
            {
                return BadRequest("La tarea no existe");
            }

            _baseDatos.Tareas.Remove(tareaEliminar);
            await _baseDatos.SaveChangesAsync();
            return Ok();
        }
        //Cross-Origin Resource Sharing

    }
}
