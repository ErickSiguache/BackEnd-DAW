using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal_DAW.Models;


namespace ProyectoFinal_DAW.Controllers
{
    public class DetalleCompraController : Controller
    {
        /// <summary>
        /// Extraccion del contexto y llenado de una variable con el.
        /// </summary>
        private readonly ProyectoFinalContext _contexto;
        public DetalleCompraController(ProyectoFinalContext miContexto)
        {
            this._contexto = miContexto;
        }

        /// <summary>
        /// Metodo de insertar
        /// </summary>
        /// <param name="detNuevo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/detallecompra")]
        public IActionResult guardarDetalle([FromBody] DetalleCompra detNuevo)
        {
            try
            {
                _contexto.DetalleCompra.Add(detNuevo);
                _contexto.SaveChanges();
                return Ok(detNuevo);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
    }
}
