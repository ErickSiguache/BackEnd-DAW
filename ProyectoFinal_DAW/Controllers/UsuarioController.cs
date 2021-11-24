using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal_DAW.Models;

namespace ProyectoFinal_DAW.Controllers
{
    public class UsuarioController : Controller
    {
        /// <summary>
        /// Extraccion del contexto y llenado de una variable con el.
        /// </summary>
        private readonly ProyectoFinalContext _contexto;
        public UsuarioController(ProyectoFinalContext miContexto)
        {
            this._contexto = miContexto;
        }

        /// <summary>
        /// Metodo busqueda en general
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/usuario")]
        public IActionResult Get()
        {
            var usuario = from e in _contexto.Usuario
                          join tipU in _contexto.TipoUsuario on e.Id_TipoUsuario equals tipU.Id_TipoUsuario
                          select new { 
                              e.Id_Usuario,
                              e.User_Nombre,
                              e.User_Apellido,
                              e.User_Direccion,
                              e.User_Telefono,
                              e.User_DUI,
                              tipU.TipU_Nombre,
                              e.UserName,
                              e.Contrasena,
                              e.User_Estado,
                              e.FechaCreacion,
                              e.FechaModificacion
                          };
            if (usuario.Count() > 0)
            {
                return Ok(usuario);
            }
            return NotFound();
        }

        /// <summary>
        /// Metodo busqueda por nombre
        /// </summary>
        /// <param name="buscarUsuario"> </param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/usuario/buscarUsuario/{buscarUsuario}")]
        public IActionResult obtenerNombre(string buscarUsuario)
        {
            var usuarioPorNombre = from e in _contexto.Usuario
                          join tipU in _contexto.TipoUsuario on e.Id_TipoUsuario equals tipU.Id_TipoUsuario
                          where e.User_Nombre.Contains(buscarUsuario)
                          select new
                          {
                              e.Id_Usuario,
                              e.User_Nombre,
                              e.User_Apellido,
                              e.User_Direccion,
                              e.User_Telefono,
                              e.User_DUI,
                              tipU.TipU_Nombre,
                              e.UserName,
                              e.Contrasena,
                              e.User_Estado
                          };
            if (usuarioPorNombre.Count() > 0)
            {
                return Ok(usuarioPorNombre);
            }
            return NotFound();
        }

        /// <summary>
        /// Metodo de insertar
        /// </summary>
        /// <param name="usuarioNuevo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/usuario")]
        public IActionResult guardarUsuario([FromBody] Usuario usuarioNuevo)
        {
            try
            {
                IEnumerable<Usuario> usuarioExiste = from e in _contexto.Usuario
                                                     where e.User_Nombre == usuarioNuevo.User_Nombre
                                                       && e.User_Apellido == usuarioNuevo.User_Apellido || 
                                                        e.UserName == usuarioNuevo.UserName ||
                                                        e.User_DUI == usuarioNuevo.User_DUI ||
                                                        e.User_Telefono == usuarioNuevo.User_Telefono
                                                     select e;

                if (usuarioExiste.Count() == 0)
                {
                    if (usuarioNuevo.User_Nombre == "" || usuarioNuevo.User_Apellido == "" || usuarioNuevo.UserName == "" || usuarioNuevo.User_DUI == "" || usuarioNuevo.Contrasena == "")
                    {
                        return Ok("No puede insertar datos vacío");
                    }
                    else
                    {
                        var fechaYhora = DateTime.Now;
                        Md5Encriptacion md5Encriptacion = new Md5Encriptacion();
                        usuarioNuevo.Contrasena = md5Encriptacion.GetMD5(usuarioNuevo.Contrasena);
                        usuarioNuevo.FechaCreacion = fechaYhora;
                        usuarioNuevo.FechaModificacion = fechaYhora;
                        _contexto.Usuario.Add(usuarioNuevo);
                        _contexto.SaveChanges();
                        return Ok(usuarioNuevo);
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
        /// <param name="usuarioAModificar"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/usuario")]
        public IActionResult updateUsuario([FromBody] Usuario usuarioAModificar)
        {
            try
            {
                IEnumerable<Usuario> usuarioExisteComparacion = from e in _contexto.Usuario
                                                     where e.User_Nombre == usuarioAModificar.User_Nombre
                                                       && e.User_Apellido == usuarioAModificar.User_Apellido 
                                                       && e.Id_Usuario != usuarioAModificar.Id_Usuario ||
                                                        e.UserName == usuarioAModificar.UserName && e.Id_Usuario != usuarioAModificar.Id_Usuario ||
                                                        e.User_DUI == usuarioAModificar.User_DUI && e.Id_Usuario != usuarioAModificar.Id_Usuario ||
                                                        e.User_Telefono == usuarioAModificar.User_Telefono && e.Id_Usuario != usuarioAModificar.Id_Usuario
                                                     select e;

                Usuario usuarioExiste = (from e in _contexto.Usuario
                                         where e.Id_Usuario == usuarioAModificar.Id_Usuario
                                         select e).FirstOrDefault();

                if (usuarioExiste != null)
                {
                    if (usuarioExisteComparacion.Count() == 0)
                    {
                        var fechaYhora = DateTime.Now;
                        Md5Encriptacion md5Encriptacion = new Md5Encriptacion();
                        usuarioExiste.Contrasena = md5Encriptacion.GetMD5(usuarioAModificar.Contrasena);
                        usuarioExiste.User_Nombre = usuarioAModificar.User_Nombre;
                        usuarioExiste.User_Apellido = usuarioAModificar.User_Apellido;
                        usuarioExiste.User_Direccion = usuarioAModificar.User_Direccion;
                        usuarioExiste.User_Telefono = usuarioAModificar.User_Telefono;
                        usuarioExiste.User_DUI = usuarioAModificar.User_DUI;
                        usuarioExiste.Id_TipoUsuario = usuarioAModificar.Id_TipoUsuario;
                        usuarioExiste.UserName = usuarioAModificar.UserName;
                        usuarioExiste.User_Estado = usuarioAModificar.User_Estado;
                        usuarioExiste.FechaModificacion = fechaYhora;

                        _contexto.Entry(usuarioExiste).State = EntityState.Modified;
                        _contexto.SaveChanges();

                        return Ok(usuarioExiste);
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
        [Route("api/usuario/{EliminarId}")]
        public IActionResult deleteUsuario(int EliminarId)
        {
            try
            {
                Usuario unUsuario = (from e in _contexto.Usuario
                                     where e.Id_Usuario == EliminarId
                                     select e).FirstOrDefault();
                if (unUsuario != null)
                {
                    _contexto.Entry(unUsuario).State = EntityState.Deleted;
                    _contexto.SaveChanges();

                    return Ok("El usuario ha sido eliminado correctamente");
                }
                return NotFound();
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        private static Random random = new Random();
        /// <summary>
        /// Metodo busqueda por nombre
        /// </summary>
        /// <param name="username"> </param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/usuario/login/{username}/{pass}")]
        public IActionResult login(string username, string pass)
        {
            Usuario loginUser = (from e in _contexto.Usuario
                                   where e.UserName.Contains(username) && e.Contrasena.Contains(pass)
                                   select e).FirstOrDefault();
            if (loginUser != null)
            {
                Guid cadena = Guid.NewGuid();
                string GuidString = Convert.ToBase64String(cadena.ToByteArray());
                GuidString = GuidString.Replace("=", "");
                GuidString = GuidString.Replace("+", "");


                loginUser.token = GuidString;
                _contexto.Entry(loginUser).State = EntityState.Modified;
                _contexto.SaveChanges();

                if(loginUser.token != "" && loginUser.UserName == username && loginUser.Contrasena == pass)
                {
                    var userLog = from e in _contexto.Usuario
                                  where e.UserName.Contains(username) && e.Contrasena.Contains(pass)
                                  select new
                                  {
                                      e.Id_TipoUsuario,
                                      e.token
                                  };
                    return Ok(userLog);
                }
            }
            return Ok("Usuario no existe");
        }
    }
}
