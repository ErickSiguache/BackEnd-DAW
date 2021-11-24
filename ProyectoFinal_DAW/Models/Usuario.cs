using System;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_DAW.Models
{
    public class Usuario
    {
        [Key]
        public int Id_Usuario { get; set; }
        public string User_Nombre { get; set; }
        public string User_Apellido { get; set; }
        public string User_Direccion { get; set; }
        public string User_Telefono { get; set; }
        public string User_Mail { get; set; }
        public string User_DUI { get; set; }
        public int Id_TipoUsuario { get; set; }
        public string UserName { get; set; }
        public string Contrasena { get; set; }
        public string token { get; set; }
        public string User_Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
