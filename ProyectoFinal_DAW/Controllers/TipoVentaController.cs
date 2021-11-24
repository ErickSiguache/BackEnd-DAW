using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal_DAW.Models;

namespace ProyectoFinal_DAW.Controllers
{
    public class TipoVentaController : Controller
    {
        /// <summary>
        /// Extraccion del contexto y llenado de una variable con el.
        /// </summary>
        private readonly ProyectoFinalContext _contexto;
        public TipoVentaController(ProyectoFinalContext miContexto)
        {
            this._contexto = miContexto;
        }

        /// <summary>
        /// Metodo busqueda general
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/tipoVenta")]
        public IActionResult Get()
        {
            var tipoVenta = from e in _contexto.TipoVenta
                             select e;
            if (tipoVenta.Count() > 0)
            {
                return Ok(tipoVenta);
            }
            return NotFound();
        }

        /// <summary>
        /// Metodo busqueda por nombre
        /// </summary>
        /// <param name="buscarTipoCompra"> </param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/tipoVenta/buscarTipoVenta/{buscarTipoVenta}")]
        public IActionResult obtenerNombre(string buscarTipoVenta)
        {
            IEnumerable<TipoVenta> tipoVentaPorNombre = from e in _contexto.TipoVenta
                                                          where e.TipV_Nombre.Contains(buscarTipoVenta)
                                                          select e;
            if (tipoVentaPorNombre.Count() > 0)
            {
                return Ok(tipoVentaPorNombre);
            }
            return NotFound();
        }

        /// <summary>
        /// Metodo de insertar
        /// </summary>
        /// <param name="tipoVentaNuevo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/tipoVenta")]
        public IActionResult guardarTipoVenta([FromBody] TipoVenta tipoVentaNuevo)
        {
            try
            {
                IEnumerable<TipoVenta> tipoVentaExiste = from e in _contexto.TipoVenta
                                                           where e.TipV_Nombre == tipoVentaNuevo.TipV_Nombre
                                                           select e;

                if (tipoVentaExiste.Count() == 0)
                {
                    if (tipoVentaNuevo.TipV_Nombre == "")
                    {
                        return Ok("No puede insertar datos vacío");
                    }
                    else
                    {
                        var fechaYhora = DateTime.Now;
                        tipoVentaNuevo.FechaCreacion = fechaYhora;
                        tipoVentaNuevo.FechaModificacion = fechaYhora;
                        _contexto.TipoVenta.Add(tipoVentaNuevo);
                        _contexto.SaveChanges();
                        return Ok(tipoVentaNuevo);
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
        /// <param name="tipoVentaAModificar"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/tipoVenta")]
        public IActionResult updateTipoVenta([FromBody] TipoVenta tipoVentaAModificar)
        {
            try
            {
                IEnumerable<TipoVenta> tipoVExisteComparacion = from e in _contexto.TipoVenta
                                                                where e.TipV_Nombre == tipoVentaAModificar.TipV_Nombre
                                                                && e.Id_TipoVenta != tipoVentaAModificar.Id_TipoVenta
                                                                select e;

                TipoVenta tipoVentaExiste = (from e in _contexto.TipoVenta
                                             where e.Id_TipoVenta == tipoVentaAModificar.Id_TipoVenta
                                             select e).FirstOrDefault();
                if (tipoVentaExiste != null)
                {
                    if (tipoVExisteComparacion.Count() == 0)
                    {
                        var fechaYhora = DateTime.Now;
                        tipoVentaExiste.TipV_Nombre = tipoVentaAModificar.TipV_Nombre;
                        tipoVentaExiste.FechaModificacion = fechaYhora;

                        _contexto.Entry(tipoVentaExiste).State = EntityState.Modified;
                        _contexto.SaveChanges();

                        return Ok(tipoVentaExiste);
                    }
                    else
                    {
                        return Ok("Ya existe un registro de tipo de venta con el nombre insertado");
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
        [Route("api/tipoVenta/{EliminarId}")]
        public IActionResult deleteTipoVenta(int EliminarId)
        {
            try
            {
                TipoVenta unTipoVenta = (from e in _contexto.TipoVenta
                                            where e.Id_TipoVenta == EliminarId
                                            select e).FirstOrDefault();
                if (unTipoVenta != null)
                {
                    _contexto.Entry(unTipoVenta).State = EntityState.Deleted;
                    _contexto.SaveChanges();

                    return Ok("El tipo de venta ha sido eliminado correctamente");
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
