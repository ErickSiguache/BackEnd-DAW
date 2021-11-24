using System;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_DAW.Models
{
    public class Marca
    {
        [Key]
        public int Id_Marca { get; set; }
        public string Mar_Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
