using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal_DAW.Models;

namespace ProyectoFinal_DAW.Controllers
{
    public class TipoCompraController : Controller
    {
        /// <summary>
        /// Extraccion del contexto y llenado de una variable con el.
        /// </summary>
        private readonly ProyectoFinalContext _contexto;
        public TipoCompraController(ProyectoFinalContext miContexto)
        {
            this._contexto = miContexto;
        }

        /// <summary>
        /// Metodo busqueda general
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/tipoCompra")]
        public IActionResult Get()
        {
            var tipoCompra = from e in _contexto.TipoCompra
                          select e;
            if (tipoCompra.Count() > 0)
            {
                return Ok(tipoCompra);
            }
            return NotFound();
        }

        /// <summary>
        /// Metodo busqueda por nombre
        /// </summary>
        /// <param name="buscarTipoCompra"> </param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/tipoCompra/buscarTipoCompra/{buscarTipoCompra}")]
        public IActionResult obtenerNombre(string buscarTipoCompra)
        {
            IEnumerable<TipoCompra> tipoCompraPorNombre = from e in _contexto.TipoCompra
                                                where e.TipC_Nombre.Contains(buscarTipoCompra)
                                                select e;
            if (tipoCompraPorNombre.Count() > 0)
            {
                return Ok(tipoCompraPorNombre);
            }
            return NotFound();
        }

        /// <summary>
        /// Metodo de insertar
        /// </summary>
        /// <param name="tipoCompraNuevo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/tipoCompra")]
        public IActionResult guardarTipoCompra([FromBody] TipoCompra tipoCompraNuevo)
        {
            try
            {
                IEnumerable<TipoCompra> tipoCompraExiste = from e in _contexto.TipoCompra
                                                     where e.TipC_Nombre == tipoCompraNuevo.TipC_Nombre 
                                                     select e;

                if (tipoCompraExiste.Count() == 0)
                {
                    if (tipoCompraNuevo.TipC_Nombre == "")
                    {
                        return Ok("No puede insertar datos vacío");
                    }
                    else
                    {
                        var fechaYhora = DateTime.Now;
                        tipoCompraNuevo.FechaCreacion = fechaYhora;
                        tipoCompraNuevo.FechaModificacion = fechaYhora;
                        _contexto.TipoCompra.Add(tipoCompraNuevo);
                        _contexto.SaveChanges();
                        return Ok(tipoCompraNuevo);
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
        /// <param name="tipoCompraAModificar"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/tipoCompra")]
        public IActionResult updateTipoCompra([FromBody] TipoCompra tipoCompraAModificar)
        {
            try
            {
                IEnumerable<TipoCompra> tipoCExisteComparacion = from e in _contexto.TipoCompra
                                                                 where e.TipC_Nombre == tipoCompraAModificar.TipC_Nombre
                                                                 && e.Id_TipoCompra != tipoCompraAModificar.Id_TipoCompra
                                                                 select e;

                TipoCompra tipoCompraExiste = (from e in _contexto.TipoCompra
                                               where e.Id_TipoCompra == tipoCompraAModificar.Id_TipoCompra
                                               select e).FirstOrDefault();
                 
                if (tipoCompraExiste != null)
                {
                    if (tipoCExisteComparacion.Count() == 0)
                    {
                        var fechaYhora = DateTime.Now;
                        tipoCompraExiste.TipC_Nombre = tipoCompraAModificar.TipC_Nombre;
                        tipoCompraExiste.FechaModificacion = fechaYhora;

                        _contexto.Entry(tipoCompraExiste).State = EntityState.Modified;
                        _contexto.SaveChanges();

                        return Ok(tipoCompraExiste);
                    }
                    else
                    {
                        return Ok("Ya existe un registro de tipo de compra con el nombre insertado");
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
        [Route("api/tipoCompra/{EliminarId}")]
        public IActionResult deleteTipoCompra(int EliminarId)
        {
            try
            {
                TipoCompra unaTipoCompra = (from e in _contexto.TipoCompra
                                  where e.Id_TipoCompra == EliminarId
                                  select e).FirstOrDefault();
                if (unaTipoCompra != null)
                {
                    _contexto.Entry(unaTipoCompra).State = EntityState.Deleted;
                    _contexto.SaveChanges();

                    return Ok("El tipo de compra ha sido eliminada correctamente");
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
