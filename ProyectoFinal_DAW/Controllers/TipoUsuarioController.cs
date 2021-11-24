using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal_DAW.Models;

namespace ProyectoFinal_DAW.Controllers
{
    public class TipoUsuarioController : Controller
    {
        /// <summary>
        /// Extraccion del contexto y llenado de una variable con el.
        /// </summary>
        private readonly ProyectoFinalContext _contexto;
        public TipoUsuarioController(ProyectoFinalContext miContexto)
        {
            this._contexto = miContexto;
        }

        /// <summary>
        /// Metodo busqueda general
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/tipoUsuario")]
        public IActionResult Get()
        {
            var tipoUsuario = from e in _contexto.TipoUsuario
                             select e;
            if (tipoUsuario.Count() > 0)
            {
                return Ok(tipoUsuario);
            }
            return NotFound();
        }

        /// <summary>
        /// Metodo busqueda por nombre
        /// </summary>
        /// <param name="buscarTipoUsuario"> </param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/tipoUsuario/buscarTipoUsuario/{buscarTipoUsuario}")]
        public IActionResult obtenerNombre(string buscarTipoUsuario)
        {
            IEnumerable<TipoUsuario> tipoUsuarioPorNombre = from e in _contexto.TipoUsuario
                                                        where e.TipU_Nombre.Contains(buscarTipoUsuario)
                                                        select e;
            if (tipoUsuarioPorNombre.Count() > 0)
            {
                return Ok(tipoUsuarioPorNombre);
            }
            return NotFound();
        }

        /// <summary>
        /// Metodo de insertar
        /// </summary>
        /// <param name="tipoUsuarioNuevo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/tipoUsuario")]
        public IActionResult guardarTipoUsuario([FromBody] TipoUsuario tipoUsuarioNuevo)
        {
            try
            {
                IEnumerable<TipoUsuario> tipoUsuarioExiste = from e in _contexto.TipoUsuario
                                                           where e.TipU_Nombre == tipoUsuarioNuevo.TipU_Nombre
                                                           select e;

                if (tipoUsuarioExiste.Count() == 0)
                {
                    if (tipoUsuarioNuevo.TipU_Nombre == "")
                    {
                        return Ok("No puede insertar datos vacío");
                    }
                    else
                    {
                        var fechaYhora = DateTime.Now;
                        tipoUsuarioNuevo.FechaCreacion = fechaYhora;
                        tipoUsuarioNuevo.FechaModificacion = fechaYhora;
                        _contexto.TipoUsuario.Add(tipoUsuarioNuevo);
                        _contexto.SaveChanges();
                        return Ok(tipoUsuarioNuevo);
                    }
                }
                return Ok("El dato ingresado ya existe");
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Metodo de modificacion
        /// </summary>
        /// <param name="tipoUsuarioAModificar"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/tipoUsuario")]
        public IActionResult updateTipoUsuario([FromBody] TipoUsuario tipoUsuarioAModificar)
        {
            try
            {
                IEnumerable<TipoUsuario> tipoUExisteComparacion = from e in _contexto.TipoUsuario
                                                                where e.TipU_Nombre == tipoUsuarioAModificar.TipU_Nombre
                                                                select e;

                TipoUsuario tipoUsuarioExiste = (from e in _contexto.TipoUsuario
                                                 where e.Id_TipoUsuario == tipoUsuarioAModificar.Id_TipoUsuario
                                                 select e).FirstOrDefault();

                if (tipoUsuarioExiste != null)
                {
                    if (tipoUExisteComparacion.Count() == 0)
                    {
                        var fechaYhora = DateTime.Now;
                        tipoUsuarioExiste.TipU_Nombre = tipoUsuarioAModificar.TipU_Nombre;
                        tipoUsuarioExiste.FechaModificacion = fechaYhora;

                        _contexto.Entry(tipoUsuarioExiste).State = EntityState.Modified;
                        _contexto.SaveChanges();

                        return Ok(tipoUsuarioExiste);
                    }
                    else
                    {
                        return Ok("Ya existe un registro de tipo de usuario con el nombre insertado");
                    }
                }
                return NotFound();
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Metodo eliminar
        /// </summary>
        /// <param name="EliminarId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/tipoUsuario/{EliminarId}")]
        public IActionResult deleteTipoUsuario(int EliminarId)
        {
            try
            {
                TipoUsuario unTipoUsuario = (from e in _contexto.TipoUsuario
                                         where e.Id_TipoUsuario == EliminarId
                                         select e).FirstOrDefault();
                if (unTipoUsuario != null)
                {
                    _contexto.Entry(unTipoUsuario).State = EntityState.Deleted;
                    _contexto.SaveChanges();

                    return Ok("El tipo de usuario ha sido eliminado correctamente");
                }
                return NotFound();
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
    }
}
