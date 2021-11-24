using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal_DAW.Models;

namespace ProyectoFinal_DAW.Controllers
{
    public class ClienteController : Controller
    {
        /// <summary>
        /// Extraccion del contexto y llenado de una variable con el.
        /// </summary>
        private readonly ProyectoFinalContext _contexto;
        public ClienteController(ProyectoFinalContext miContexto)
        {
            this._contexto = miContexto;
        }
        
        /// <summary>
        /// Metodo busqueda general
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/cliente")]
        public IActionResult Get()
        {
            var cliente = from e in _contexto.Cliente
                          select e;
            if (cliente.Count() > 0)
            {
                return Ok(cliente);
            }
            return NotFound();
        }

        /// <summary>
        /// Metodo busqueda por nombre
        /// </summary>
        /// <param name="buscarCliente"> </param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/cliente/buscarCliente/{buscarCliente}")]
        public IActionResult obtenerNombre(string buscarCliente)
        {
            IEnumerable<Cliente> clientePorNombre = from e in _contexto.Cliente
                                                where e.Clie_Nombre.Contains(buscarCliente)
                                                select e;
            if (clientePorNombre.Count() > 0)
            {
                return Ok(clientePorNombre);
            }
            return NotFound();
        }

        /// <summary>
        /// Metodo busqueda por DUI
        /// </summary>
        /// <param name="buscarDUI"> </param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/cliente/buscarDUI/{buscarDUI}")]
        public IActionResult obtenerDUI(string buscarDUI)
        {
            IEnumerable<Cliente> clientePorDUI = from e in _contexto.Cliente
                                                    where e.Clie_DUI.Contains(buscarDUI)
                                                    select e;
            if (clientePorDUI.Count() > 0)
            {
                return Ok(clientePorDUI);
            }
            return NotFound();
        }

        /// <summary>
        /// Metodo de insertar
        /// </summary>
        /// <param name="clienteNuevo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/cliente")]
        public IActionResult guardarCliente([FromBody] Cliente clienteNuevo)
        {
            try
            {
                IEnumerable<Cliente> clienteExiste = from e in _contexto.Cliente
                                                       where e.Clie_Nombre == clienteNuevo.Clie_Nombre
                                                       && e.Clie_Apellido == clienteNuevo.Clie_Apellido || 
                                                       e.Telefono == clienteNuevo.Telefono || 
                                                       e.Clie_DUI == clienteNuevo.Clie_DUI
                                                       select e;
                if (clienteExiste.Count() == 0)
                {
                    if (clienteNuevo.Clie_Nombre == "" || clienteNuevo.Clie_Apellido == "" || clienteNuevo.Telefono == "" || clienteNuevo.Clie_DUI == "")
                    {
                        return Ok("No puede insertar datos vacío");
                    }
                    else
                    {
                        var fechaYhora = DateTime.Now;
                        clienteNuevo.FechaCreacion = fechaYhora;
                        clienteNuevo.FechaModificacion = fechaYhora;
                        _contexto.Cliente.Add(clienteNuevo);
                        _contexto.SaveChanges();
                        return Ok(clienteNuevo);
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
        /// <param name="clienteAModificar"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/cliente")]
        public IActionResult updateCliente([FromBody] Cliente clienteAModificar)
        {
            try
            {
                ///Consulta de las marcas para realizar una compracion
                IEnumerable<Cliente> clienteExisteComparacion = from e in _contexto.Cliente
                                                     where e.Clie_Nombre == clienteAModificar.Clie_Nombre
                                                     && e.Clie_Apellido == clienteAModificar.Clie_Apellido 
                                                     && e.Id_Cliente != clienteAModificar.Id_Cliente ||
                                                     e.Telefono == clienteAModificar.Telefono && e.Id_Cliente != clienteAModificar.Id_Cliente ||
                                                     e.Clie_DUI == clienteAModificar.Clie_DUI && e.Id_Cliente != clienteAModificar.Id_Cliente
                                                                select e;

                ///Comparacion del ID a editar la marca
                Cliente clienteExiste = (from e in _contexto.Cliente
                                         where e.Id_Cliente == clienteAModificar.Id_Cliente
                                         select e).FirstOrDefault();

                ///Compara que la marca del ID sea diferente de nulo para poder editar
                if (clienteExiste != null)
                {
                    ///Compara que el texto enviado para la edicion no sea repetido
                    if (clienteExisteComparacion.Count() == 0)
                    {
                        var fechaYhora = DateTime.Now;
                        clienteExiste.Clie_Nombre = clienteAModificar.Clie_Nombre;
                        clienteExiste.Clie_Apellido = clienteAModificar.Clie_Apellido;
                        clienteExiste.Telefono = clienteAModificar.Telefono;
                        clienteExiste.Clie_DUI = clienteAModificar.Clie_DUI;
                        clienteExiste.Referencia = clienteAModificar.Referencia;
                        clienteExiste.FechaModificacion = fechaYhora;

                        _contexto.Entry(clienteExiste).State = EntityState.Modified;
                        _contexto.SaveChanges();

                        return Ok(clienteExiste);
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
        [Route("api/cliente/{EliminarId}")]
        public IActionResult deleteCliente(int EliminarId)
        {
            try
            {
                Cliente unCliente = (from e in _contexto.Cliente
                                  where e.Id_Cliente == EliminarId
                                  select e).FirstOrDefault();
                if (unCliente != null)
                {
                    _contexto.Entry(unCliente).State = EntityState.Deleted;
                    _contexto.SaveChanges();

                    return Ok("El cliente ha sido eliminado correctamente");
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
