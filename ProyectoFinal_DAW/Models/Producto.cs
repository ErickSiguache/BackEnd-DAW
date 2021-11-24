using System;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_DAW.Models
{
    public class Producto
    {
        [Key]
        public int Id_Producto { get; set; }
        public string Pro_Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Id_Marca { get; set; }
        public int Id_Categoria { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
