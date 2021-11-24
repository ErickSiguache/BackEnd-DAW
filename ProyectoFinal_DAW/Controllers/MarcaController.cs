using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal_DAW.Models;

namespace ProyectoFinal_DAW.Controllers
{
    public class MarcaController : Controller
    {
        /// <summary>
        /// Extraccion del contexto y llenado de una variable con el.
        /// </summary>
        private readonly ProyectoFinalContext _contexto;
        public MarcaController(ProyectoFinalContext miContexto)
        {
            this._contexto = miContexto;
        }

        /// <summary>
        /// Metodo busqueda general
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/marca")]
        public IActionResult Get()
        {
            var marcas = from e in _contexto.Marca
                             select e;
            if (marcas.Count() > 0)
            {
                return Ok(marcas);
            }
            return NotFound();
        }

        /// <summary>
        /// Metodo busqueda por nombre
        /// </summary>
        /// <param name="buscarMarca"> </param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/marca/buscarMarca/{buscarMarca}")]
        public IActionResult obtenerNombre(string buscarMarca)
        {
            IEnumerable<Marca> marcaPorNombre = from e in _contexto.Marca
                                                   where e.Mar_Nombre.Contains(buscarMarca)
                                                   select e;
            if (marcaPorNombre.Count() > 0)
            {
                return Ok(marcaPorNombre);
            }
            return NotFound();
        }

        /// <summary>
        /// Metodo insertar
        /// </summary>
        /// <param name="marcaNuevo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/marca")]
        public IActionResult guardarMarca([FromBody] Marca marcaNuevo)
        {
            try
            {
                IEnumerable<Marca> marcaExiste = from e in _contexto.Marca
                                                         where e.Mar_Nombre == marcaNuevo.Mar_Nombre
                                                         select e;
                if (marcaExiste.Count() == 0)
                {
                    if(marcaNuevo.Mar_Nombre == "")
                    {
                        return Ok("No puede insertar datos vacío");
                    } else {
                        var fechaYhora = DateTime.Now;
                        marcaNuevo.FechaCreacion = fechaYhora;
                        marcaNuevo.FechaModificacion = fechaYhora;
                        _contexto.Marca.Add(marcaNuevo);
                        _contexto.SaveChanges();
                        return Ok(marcaNuevo);
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
        /// <param name="marcaAModificar"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/marca")]
        public IActionResult updateMarca([FromBody] Marca marcaAModificar)
        {
            try
            {
                ///Consulta de las marcas para realizar una compracion
                IEnumerable<Marca> marcaExisteComparacion = from e in _contexto.Marca
                                                            where e.Mar_Nombre == marcaAModificar.Mar_Nombre
                                                            select e;

                ///Comparacion del ID a editar la marca
                Marca marcaExiste = (from e in _contexto.Marca
                                     where e.Id_Marca == marcaAModificar.Id_Marca
                                     select e).FirstOrDefault();
                
                ///Compara que la marca del ID sea diferente de nulo para poder editar
                if(marcaExiste != null)
                {
                    ///Compara que el texto enviado para la edicion no sea repetido
                    if (marcaExisteComparacion.Count() == 0)
                    {
                        var fechaYhora = DateTime.Now;
                        marcaExiste.Mar_Nombre = marcaAModificar.Mar_Nombre;
                        marcaExiste.FechaModificacion = fechaYhora;

                        _contexto.Entry(marcaExiste).State = EntityState.Modified;
                        _contexto.SaveChanges();

                        return Ok(marcaExiste);
                    } else  {
                        return Ok("Ya existe una marca con el nombre insertado");
                    }
                }
                return NotFound();
            }
            catch(System.Exception)
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
        [Route("api/marca/{EliminarId}")]
        public IActionResult deleteMarca(int EliminarId)
        {
            try
            {
                Marca unaMarca = (from e in _contexto.Marca
                                  where e.Id_Marca == EliminarId
                                  select e).FirstOrDefault();
                if (unaMarca != null)
                {
                    _contexto.Entry(unaMarca).State = EntityState.Deleted;
                    _contexto.SaveChanges();

                    return Ok("La marca ha sido eliminada correctamente");
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
