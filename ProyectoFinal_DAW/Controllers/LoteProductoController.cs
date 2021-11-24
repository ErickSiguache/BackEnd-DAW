using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal_DAW.Models;

namespace ProyectoFinal_DAW.Controllers
{
    public class LoteProductoController : Controller
    {
        /// <summary>
        /// Extraccion del contexto y llenado de una variable con el.
        /// </summary>
        private readonly ProyectoFinalContext _contexto;
        public LoteProductoController(ProyectoFinalContext miContexto)
        {
            this._contexto = miContexto;
        }

        /// <summary>
        /// Metodo busqueda en general del inventario
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/inventario")]
        public IActionResult Get()
        {
            var inventario = from e in _contexto.LoteProducto
                          join prod in _contexto.Producto on e.Id_Producto equals prod.Id_Producto
                          select new
                          {
                              e.Id_LoteProducto,
                              prod.Pro_Nombre,
                              e.FechaCaducacion,
                              e.PrecioVenta,
                              e.PrecioCompra,
                              e.StockComprado,
                              e.StockVendido,
                              e.Lot_Estado,
                              e.FechaCreacion,
                              e.FechaModificacion
                          };
            if (inventario.Count() > 0)
            {
                return Ok(inventario);
            }
            return NotFound();
        }

        /// <summary>
        /// Metodo busqueda por nombre del producto
        /// </summary>
        /// <param name="buscarProducto"> </param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/cliente/inventario/{buscarProducto}")]
        public IActionResult obtenerNombre(string buscarProducto)
        {
            var inventarioPorNombre = from e in _contexto.LoteProducto
                             join prod in _contexto.Producto on e.Id_Producto equals prod.Id_Producto
                             where prod.Pro_Nombre.Contains(buscarProducto)
                             select new
                             {
                                 e.Id_LoteProducto,
                                 prod.Pro_Nombre,
                                 e.FechaCaducacion,
                                 e.PrecioVenta,
                                 e.PrecioCompra,
                                 e.StockComprado,
                                 e.StockVendido,
                                 e.Lot_Estado,
                                 e.FechaCreacion,
                                 e.FechaModificacion
                             };
            if (inventarioPorNombre.Count() > 0)
            {
                return Ok(inventarioPorNombre);
            }
            return NotFound();
        }

        /// <summary>
        /// Metodo productos mas vendidos al menos vendido
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/inventario/MasVendidosAMenos")]
        public IActionResult getbyStockMax()
        {
            var stockDisponibleMax = from e in _contexto.LoteProducto
                                     join prod in _contexto.Producto on e.Id_Producto equals prod.Id_Producto
                                     orderby e.StockVendido descending
                                     select new
                                     {
                                         e.Id_LoteProducto,
                                         prod.Pro_Nombre,
                                         e.FechaCaducacion,
                                         e.PrecioVenta,
                                         e.PrecioCompra,
                                         e.StockComprado,
                                         e.StockVendido,
                                         e.Lot_Estado,
                                         e.FechaCreacion,
                                         e.FechaModificacion
                                     };
            if (stockDisponibleMax != null)
            {
                return Ok(stockDisponibleMax);
            }
            return NotFound();
        }

        /// <summary>
        /// Metodo productos mas vendidos al menos vendido
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/inventario/MenosVendidosAMas")]
        public IActionResult getbyStockMin()
        {
            var stockDisponibleMin = from e in _contexto.LoteProducto
                                     join prod in _contexto.Producto on e.Id_Producto equals prod.Id_Producto
                                     orderby e.StockVendido ascending
                                     select new
                                     {
                                         e.Id_LoteProducto,
                                         prod.Pro_Nombre,
                                         e.FechaCaducacion,
                                         e.PrecioVenta,
                                         e.PrecioCompra,
                                         e.StockComprado,
                                         e.StockVendido,
                                         e.Lot_Estado,
                                         e.FechaCreacion,
                                         e.FechaModificacion
                                     };

            if (stockDisponibleMin != null)
            {
                return Ok(stockDisponibleMin);
            }
            return NotFound();
        }
    }
}
