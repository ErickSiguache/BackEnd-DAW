using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal_DAW.Models;

namespace ProyectoFinal_DAW.Controllers
{
    public class CategoriaController : Controller
    {
        /// <summary>
        /// Extraccion del contexto y llenado de una variable con el.
        /// </summary>
        private readonly ProyectoFinalContext _contexto;
        public CategoriaController(ProyectoFinalContext miContexto)
        {
            this._contexto = miContexto;
        }

        [HttpGet]
        [Route("api/categoria")]
        public IActionResult Get()
        {
            var categorias = from e in _contexto.Categoria
                             select e;
            if(categorias.Count() > 0)
            {
                return Ok(categorias);
            }
            return NotFound();
        }

        /// <summary>
        /// Metodo busqueda por nombre
        /// </summary>
        /// <param name="buscarCategoria"> </param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/categoria/buscarCategoria/{buscarCategoria}")]
        public IActionResult obtenerNombre(string buscarCategoria)
        {
            IEnumerable<Categoria> categoriaPorNombre = from e in _contexto.Categoria
                                                where e.Cat_Nombre.Contains(buscarCategoria)
                                                select e;
            if (categoriaPorNombre.Count() > 0)
            {
                return Ok(categoriaPorNombre);
            }
            return NotFound();
        }

        /// <summary>
        /// Metodo de insertar
        /// </summary>
        /// <param name="categoriaNuevo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/categoria")]
        public IActionResult guardarCategoria([FromBody] Categoria categoriaNuevo)
        {
            try
            {
                IEnumerable<Categoria> categoriaExiste = from e in _contexto.Categoria
                                                     where e.Cat_Nombre == categoriaNuevo.Cat_Nombre
                                                     select e;
                if (categoriaExiste.Count() == 0)
                {
                    if(categoriaNuevo.Cat_Nombre == "")
                    {
                        return Ok("No puede insertar datos vacío");
                    } else {
                        var fechaYhora = DateTime.Now;
                        categoriaNuevo.FechaCreacion = fechaYhora;
                        categoriaNuevo.FechaModificacion = fechaYhora;
                        _contexto.Categoria.Add(categoriaNuevo);
                        _contexto.SaveChanges();
                        return Ok(categoriaNuevo);
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
        /// <param name="categoriaAModificar"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/categoria")]
        public IActionResult updatCategoria([FromBody] Categoria categoriaAModificar)
        {
            try
            {
                ///Consulta de las categoria para realizar una compracion
                IEnumerable<Categoria> categoriaExisteComparacion = from e in _contexto.Categoria
                                                            where e.Cat_Nombre == categoriaAModificar.Cat_Nombre
                                                            select e;

                ///Comparacion del ID a editar la marca
                Categoria categoriaExiste = (from e in _contexto.Categoria
                                             where e.id_Categoria == categoriaAModificar.id_Categoria
                                             select e).FirstOrDefault();

                ///Compara que la categoria del ID sea diferente de nulo para poder editar
                if (categoriaExiste != null)
                {
                    ///Compara que el texto enviado para la edicion no sea repetido
                    if (categoriaExisteComparacion.Count() == 0)
                    {
                        var fechaYhora = DateTime.Now;
                        categoriaExiste.Cat_Nombre = categoriaAModificar.Cat_Nombre;
                        categoriaExiste.FechaModificacion = fechaYhora;

                        _contexto.Entry(categoriaExiste).State = EntityState.Modified;
                        _contexto.SaveChanges();

                        return Ok(categoriaExiste);
                    }
                    else
                    {
                        return Ok("Ya existe una categoria con el nombre insertado");
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
        /// Metodo Eliminar
        /// </summary>
        /// <param name="EliminarId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/categoria/{EliminarId}")]
        public IActionResult deleteCategoria(int EliminarId)
        {
            try
            {
                Categoria unaCategoria = (from e in _contexto.Categoria
                                  where e.id_Categoria == EliminarId
                                  select e).FirstOrDefault();
                if (unaCategoria != null)
                {
                    _contexto.Entry(unaCategoria).State = EntityState.Deleted;
                    _contexto.SaveChanges();

                    return Ok("La categoria ha sido eliminada correctamente");
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
