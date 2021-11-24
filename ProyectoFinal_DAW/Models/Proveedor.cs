using System;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_DAW.Models
{
    public class Proveedor
    {
        [Key]
        public int Id_Proveedor { get; set; }
        public string Prov_Nombre { get; set; }
        public string Prov_Telefono { get; set; }
        public string Prov_Direccion { get; set; }
        public string E_Mail { get; set; }
        public string NRegistro { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
