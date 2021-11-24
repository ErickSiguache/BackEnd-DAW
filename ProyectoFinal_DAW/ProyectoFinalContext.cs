using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using ProyectoFinal_DAW.Models;

namespace ProyectoFinal_DAW
{
    public class ProyectoFinalContext : DbContext
    {
        public ProyectoFinalContext(DbContextOptions<ProyectoFinalContext> options) : base(options) 
        { }

        //Contexto de los metodos
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Marca> Marca { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<TipoUsuario> TipoUsuario { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Proveedor> Proveedor { get; set; }
        public DbSet<LoteProducto> LoteProducto { get; set; }
        public DbSet<TipoCompra> TipoCompra { get; set; }
        public DbSet<TipoVenta> TipoVenta { get; set; }
        public DbSet<Compra> Compra { get; set; }
        public DbSet<DetalleCompra> DetalleCompra { get; set; }
        public DbSet<Venta> Venta { get; set; }
        public DbSet<DetalleVenta> DetalleVenta { get; set; }
    }
}
