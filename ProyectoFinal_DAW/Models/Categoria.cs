using System;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_DAW.Models
{
    public class Categoria
    {
        [Key]
        public int id_Categoria { get; set; }
        public string Cat_Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
