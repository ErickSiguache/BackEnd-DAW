using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal_DAW.Models;

namespace ProyectoFinal_DAW.Controllers
{
    public class ProveedorController : Controller
    {
        /// <summary>
        /// Extraccion del contexto y llenado de una variable con el.
        /// </summary>
        private readonly ProyectoFinalContext _contexto;
        public ProveedorController(ProyectoFinalContext miContexto)
        {
            this._contexto = miContexto;
        }

        /// <summary>
        /// Busqueda en general
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/proveedor")]
        public IActionResult Get()
        {
            var proveedor = from e in _contexto.Proveedor
                             select e;
            if (proveedor.Count() > 0)
            {
                return Ok(proveedor);
            }
            return NotFound();
        }

        /// <summary>
        /// Metodo busqueda por nombre
        /// </summary>
        /// <param name="buscarProveedor"> </param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/cliente/buscarProveedor/{buscarProveedor}")]
        public IActionResult obtenerNombre(string buscarProveedor)
        {
            IEnumerable<Proveedor> proveedorPorNombre = from e in _contexto.Proveedor
                                                    where e.Prov_Nombre.Contains(buscarProveedor)
                                                    select e;
            if (buscarProveedor.Count() > 0)
            {
                return Ok(proveedorPorNombre);
            }
            return NotFound();
        }

        /// <summary>
        /// Metodo busqueda por Numero de registro
        /// </summary>
        /// <param name="buscarNRegistro"> </param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/cliente/buscarNRegistro/{buscarNRegistro}")]
        public IActionResult obtenerNRegistro(string buscarNRegistro)
        {
            IEnumerable<Proveedor> proveedorPorNRegistro = from e in _contexto.Proveedor
                                                 where e.NRegistro.Contains(buscarNRegistro)
                                                 select e;
            if (proveedorPorNRegistro.Count() > 0)
            {
                return Ok(proveedorPorNRegistro);
            }
            return NotFound();
        }

        /// <summary>
        /// Metodo de insertar
        /// </summary>
        /// <param name="proveedorNuevo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/proveedor")]
        public IActionResult guardarProveedor([FromBody] Proveedor proveedorNuevo)
        {
            try
            {
                IEnumerable<Proveedor> proveedorExiste = from e in _contexto.Proveedor
                                                         where e.NRegistro == proveedorNuevo.NRegistro
                                                         || e.Prov_Telefono == proveedorNuevo.Prov_Telefono
                                                         || e.E_Mail == proveedorNuevo.E_Mail
                                                         select e;

                if (proveedorExiste.Count() == 0)
                {
                    if(proveedorNuevo.Prov_Nombre == "" || proveedorNuevo.Prov_Telefono == "" || proveedorNuevo.E_Mail == ""  || proveedorNuevo.NRegistro == "")
                    {
                        return Ok("No puede dejar campos vacío");
                    }
                    else
                    {
                        var fechaYhora = DateTime.Now;
                        proveedorNuevo.FechaCreacion = fechaYhora;
                        proveedorNuevo.FechaModificacion = fechaYhora;
                        _contexto.Proveedor.Add(proveedorNuevo);
                        _contexto.SaveChanges();
                        return Ok(proveedorNuevo);
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
        /// <param name="proveedorAModificar"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/proveedor")]
        public IActionResult updateProveedor([FromBody] Proveedor proveedorAModificar)
        {
            try
            {
                ///Consulta de las marcas para realizar una compracion
                IEnumerable<Proveedor> proveedorExisteComparacion = from e in _contexto.Proveedor
                                                                    where e.NRegistro == proveedorAModificar.NRegistro && e.Id_Proveedor != proveedorAModificar.Id_Proveedor
                                                                    || e.Prov_Telefono == proveedorAModificar.Prov_Telefono && e.Id_Proveedor != proveedorAModificar.Id_Proveedor
                                                                    || e.E_Mail == proveedorAModificar.E_Mail && e.Id_Proveedor != proveedorAModificar.Id_Proveedor
                                                                    select e;

                ///Comparacion del ID a editar la marca
                Proveedor proveedorExiste = (from e in _contexto.Proveedor
                                             where e.Id_Proveedor == proveedorAModificar.Id_Proveedor
                                             select e).FirstOrDefault();
                if (proveedorExiste != null)
                {
                    if (proveedorExisteComparacion.Count() == 0)
                    {
                        var fechaYhora = DateTime.Now;
                        proveedorExiste.Prov_Nombre = proveedorAModificar.Prov_Nombre;
                        proveedorExiste.Prov_Telefono = proveedorAModificar.Prov_Telefono;
                        proveedorExiste.Prov_Direccion = proveedorAModificar.E_Mail;
                        proveedorExiste.NRegistro = proveedorAModificar.NRegistro;
                        proveedorExiste.FechaModificacion = fechaYhora;

                        _contexto.Entry(proveedorExiste).State = EntityState.Modified;
                        _contexto.SaveChanges();

                        return Ok(proveedorExiste);
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
        [Route("api/proveedor/{EliminarId}")]
        public IActionResult deleteProveedor(int EliminarId)
        {
            try
            {
                Proveedor unProveedor = (from e in _contexto.Proveedor
                                     where e.Id_Proveedor == EliminarId
                                     select e).FirstOrDefault();
                if (unProveedor != null)
                {
                    _contexto.Entry(unProveedor).State = EntityState.Deleted;
                    _contexto.SaveChanges();

                    return Ok("El proveedor ha sido eliminado correctamente");
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
