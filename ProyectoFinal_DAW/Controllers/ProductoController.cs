using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal_DAW.Models;

namespace ProyectoFinal_DAW.Controllers
{
    public class ProductoController : Controller
    {
        /// <summary>
        /// Extraccion del contexto y llenado de una variable con el.
        /// </summary>
        private readonly ProyectoFinalContext _contexto;
        public ProductoController(ProyectoFinalContext miContexto)
        {
            this._contexto = miContexto;
        }

        /// <summary>
        /// Metodo busqueda en general
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/producto")]
        public IActionResult Get()
        {
            var productos = from e in _contexto.Producto
                            join mar in _contexto.Marca on e.Id_Marca equals mar.Id_Marca
                            join cat in _contexto.Categoria on e.Id_Categoria equals cat.id_Categoria
                            select new
                            {
                                e.Id_Producto,
                                e.Pro_Nombre,
                                e.Descripcion,
                                mar.Mar_Nombre,
                                cat.Cat_Nombre,
                                e.FechaCreacion,
                                e.FechaModificacion
                            };
            if (productos.Count() > 0)
            {
                return Ok(productos);
            }
            return NotFound();
        }

        /// <summary>
        /// Metodo busqueda por nombre
        /// </summary>
        /// <param name="buscarProducto"> </param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/cliente/buscarProducto/{buscarProducto}")]
        public IActionResult obtenerNombre(string buscarProducto)
        {
            var productoPorNombre = from e in _contexto.Producto
                            join mar in _contexto.Marca on e.Id_Marca equals mar.Id_Marca
                            join cat in _contexto.Categoria on e.Id_Categoria equals cat.id_Categoria
                            where e.Pro_Nombre.Contains(buscarProducto)
                            select new
                            {
                                e.Id_Producto,
                                e.Pro_Nombre,
                                e.Descripcion,
                                mar.Mar_Nombre,
                                cat.Cat_Nombre,
                                e.FechaCreacion,
                                e.FechaModificacion
                            };
            if (productoPorNombre.Count() > 0)
            {
                return Ok(productoPorNombre);
            }
            return NotFound();
        }

        /// <summary>
        /// Metodo busqueda por marca
        /// </summary>
        /// <param name="buscarProMarca"> </param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/cliente/buscarProMarca/{buscarProMarca}")]
        public IActionResult obtenerMarca(string buscarProMarca)
        {
            var productoPorMarca = from e in _contexto.Producto
                                    join mar in _contexto.Marca on e.Id_Marca equals mar.Id_Marca
                                    join cat in _contexto.Categoria on e.Id_Categoria equals cat.id_Categoria
                                    where mar.Mar_Nombre.Contains(buscarProMarca)
                                    select new
                                    {
                                        e.Id_Producto,
                                        e.Pro_Nombre,
                                        e.Descripcion,
                                        mar.Mar_Nombre,
                                        cat.Cat_Nombre,
                                        e.FechaCreacion,
                                        e.FechaModificacion
                                    };
            if (productoPorMarca.Count() > 0)
            {
                return Ok(productoPorMarca);
            }
            return NotFound();
        }

        /// <summary>
        /// Metodo busqueda por categoria
        /// </summary>
        /// <param name="buscarProCat"> </param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/cliente/buscarProCat/{buscarProCat}")]
        public IActionResult obtenerCategoria(string buscarProCat)
        {
            var productoPorCategoria = from e in _contexto.Producto
                                   join mar in _contexto.Marca on e.Id_Marca equals mar.Id_Marca
                                   join cat in _contexto.Categoria on e.Id_Categoria equals cat.id_Categoria
                                   where cat.Cat_Nombre.Contains(buscarProCat)
                                   select new
                                   {
                                       e.Id_Producto,
                                       e.Pro_Nombre,
                                       e.Descripcion,
                                       mar.Mar_Nombre,
                                       cat.Cat_Nombre,
                                       e.FechaCreacion,
                                       e.FechaModificacion
                                   };
            if (productoPorCategoria.Count() > 0)
            {
                return Ok(productoPorCategoria);
            }
            return NotFound();
        }

        /// <summary>
        /// Metodo de insertar
        /// </summary>
        /// <param name="productoNuevo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/producto")]
        public IActionResult guardarProducto([FromBody] Producto productoNuevo)
        {
            try
            {
                IEnumerable<Producto> productoExiste = from e in _contexto.Producto
                                                 where e.Pro_Nombre == productoNuevo.Pro_Nombre
                                                 select e;

                if (productoExiste.Count() == 0)
                {
                    if (productoNuevo.Pro_Nombre == "")
                    {
                        return Ok("No puede insertar datos vacío");
                    }
                    else
                    {
                        var fechaYhora = DateTime.Now;
                        productoNuevo.FechaCreacion = fechaYhora;
                        productoNuevo.FechaModificacion = fechaYhora;
                        _contexto.Producto.Add(productoNuevo);
                        _contexto.SaveChanges();
                        return Ok(productoNuevo);
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
        /// <param name="productoAModificar"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/producto")]
        public IActionResult updateProducto([FromBody] Producto productoAModificar)
        {
            try
            {
                IEnumerable<Producto> productoExisteComparacion = from e in _contexto.Producto
                                                                  where e.Pro_Nombre == productoAModificar.Pro_Nombre 
                                                                  && e.Id_Producto != productoAModificar.Id_Producto
                                                                  select e;

                Producto productoExiste = (from e in _contexto.Producto
                                           where e.Id_Producto == productoAModificar.Id_Producto
                                           select e).FirstOrDefault();

                if (productoExiste != null)
                {
                    if (productoExisteComparacion.Count() == 0)
                    {
                        var fechaYhora = DateTime.Now;
                        productoExiste.Pro_Nombre = productoAModificar.Pro_Nombre;
                        productoExiste.Descripcion = productoAModificar.Descripcion;
                        productoExiste.Id_Marca = productoAModificar.Id_Marca;
                        productoExiste.Id_Categoria = productoAModificar.Id_Categoria;
                        productoExiste.FechaModificacion = fechaYhora;

                        _contexto.Entry(productoExiste).State = EntityState.Modified;
                        _contexto.SaveChanges();

                        return Ok(productoExiste);
                    }
                    else
                    {
                        return Ok("Uno de los datos ingresados ya existe en la base de datos");
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
        [Route("api/producto/{EliminarId}")]
        public IActionResult deleteProducto(int EliminarId)
        {
            try
            {
                Producto unProducto = (from e in _contexto.Producto
                                     where e.Id_Producto == EliminarId
                                     select e).FirstOrDefault();
                if (unProducto != null)
                {
                    _contexto.Entry(unProducto).State = EntityState.Deleted;
                    _contexto.SaveChanges();

                    return Ok("El producto ha sido eliminado correctamente");
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
