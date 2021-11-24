using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal_DAW.Models;
using Newtonsoft.Json;

namespace ProyectoFinal_DAW.Controllers
{
    public class CompraController : Controller
    {
        /// <summary>
        /// Extraccion del contexto y llenado de una variable con el.
        /// </summary>
        private readonly ProyectoFinalContext _contexto;
        public CompraController(ProyectoFinalContext miContexto)
        {
            this._contexto = miContexto;
        }

        /// <summary>
        /// Metodo de insertar una compra
        /// </summary>
        /// <param name="compraNuevo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/compra/Nuevo/")]
        public IActionResult guardarCompra2([FromBody] Compra compraNuevo)
        {
            try
            {
                var fechaYhora = DateTime.Now;
                Compra compra = new Compra
                {
                    Id_Proveedor = compraNuevo.Id_Proveedor,
                    Id_Usuario = compraNuevo.Id_Usuario,
                    TotalCompra = compraNuevo.TotalCompra,
                    FechaCompra = fechaYhora,
                    Id_TipoCompra = compraNuevo.Id_TipoCompra,
                    FechaCreacion = fechaYhora,
                    FechaModificacion = fechaYhora
                };

                _contexto.Compra.Add(compra);
                _contexto.SaveChanges();
                return Ok(compra);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Revisar https://es.stackoverflow.com/questions/358894/convertir-list-a-json-httppost
        /// </summary>
        /// <param name="compra"></param>
        /// <param name="detalles"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/compra/")]
        public IActionResult InserCompra([FromBody] Compra compra)
        {
            try
            {

                var fechaYhora = DateTime.Now;
                Compra compraD = new Compra();
                compraD.Id_Proveedor = compra.Id_Proveedor;
                compraD.Id_TipoCompra = compra.Id_TipoCompra;
                compraD.Id_Usuario = compra.Id_Usuario;
                compraD.FechaCompra = fechaYhora;
                compraD.TotalCompra = compra.TotalCompra;
                compraD.Comp_Estado = compra.Comp_Estado;
                compraD.FechaCreacion = fechaYhora;
                compraD.FechaModificacion = fechaYhora;
                _contexto.Compra.Add(compraD);
                _contexto.SaveChanges();

                foreach(var detalle in compra.detalleCompras)
                {
                    DetalleCompra detalleCompra = new DetalleCompra();
                    detalleCompra.Id_Compra = compraD.Id_Compra;
                    detalleCompra.Id_LoteProducto = detalle.Id_LoteProducto;
                    detalleCompra.Comp_Cantidad = detalle.Comp_Cantidad;
                    detalleCompra.Comp_Precio = detalle.Comp_Precio;
                    detalleCompra.FechaCreacion = fechaYhora;
                    detalleCompra.FechaModificacion = fechaYhora;
                    _contexto.DetalleCompra.Add(detalleCompra);
                    _contexto.SaveChanges();
                }
                return Ok(compraD);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
    }
}
