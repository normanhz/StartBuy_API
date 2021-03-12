using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using STARTBUY_API.Classes;
using STARTBUY_API.Models.DTOs;

namespace STARTBUY_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly StartBuyContext _context;

        AppServices _AppServices = new AppServices();

        public UserController(StartBuyContext context)
        {
            _context = context;
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult> GetUsers()
        {
            var user = await _context.TblUsuariosPersonas.ToListAsync();
            return Ok(user);
        }

        [HttpGet("GetInfoUsuarioPersona/{username}/{email}")]
        public async Task<ActionResult> GetInfoUsuarioPersona(string username, string email)
        {
            var user = await _context.TblUsuariosPersonas.Where(x=> x.Usuario == username && x.Email == email).ToListAsync();
            return Ok(user);
        }

        [HttpGet("GetInfoUsuarioSocio/{username}/{email}")]
        public async Task<ActionResult> GetInfoUsuarioSocio(string username, string email)
        {
            var user = await _context.TblUsuariosAsociados.Where(x => x.Usuario == username && x.Email == email).ToListAsync();
            return Ok(user);
        }


        [HttpGet("GetGenders")]
        public async Task<ActionResult> GetGenders()
        {
            var generos = await _context.TblGeneros.ToListAsync();
            return Ok(generos);
        }

        // TRABAJAAA EN ESTO NORMAN DEL FUTURO NO SEAS HUEVON.!, YAAAA NORMAN DEL PASADO YA LO HICE!!
        [HttpPost("UserLoginv2")]
        public async Task<ActionResult<TblUsuarios>> PostUserLoginv2(LoginDTO log)
        {
            var passencrypted = Encrypter.Encrypt(log.Password);

            var user = await _context.TblUsuarios.FirstOrDefaultAsync(x => x.Email == log.User && x.Password == passencrypted || x.Usuario == log.User && x.Password == passencrypted);

            var validar = user == null;
            if (validar)
            {
                return NotFound();
            }

            return Ok(new
            {
                UsuarioId = user.UsuarioId,
                Usuario = user.Usuario,
                Nombres = user.Nombres,
                Email = user.Email,
                Password = user.Password,
                IsAdmin = user.IsAdmin,
            });
        }

      

        //[HttpPost("UserLogin")]
        //public async Task<ActionResult<TblUsuariosPersonas>> PostUserLogin(LoginDTO log)
        //{
        //    var passencrypted = Encrypter.Encrypt(log.Password);

        //    var user = await _context.TblUsuariosPersonas.FirstOrDefaultAsync(x => x.Email == log.User && x.Password == passencrypted || x.Usuario == log.User && x.Password == passencrypted);

        //    var validar = user == null;
        //    if (validar)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(new
        //    {
        //        UsuarioPersonaId = user.UsuarioPersonaId,
        //        Usuario = user.Usuario,
        //        Nombres = user.Nombres,
        //        Apellidos = user.Apellidos,
        //        Email = user.Email,
        //        GeneroId = user.GeneroId,
        //        PaisId = user.PaisId,
        //        Password = user.Password,
        //        CodigoVerificacion = user.CodigoVerificacion,
        //        DepartamentoId = user.DepartamentoId,
        //        CiudadId = user.CiudadId,
        //        DireccionCompleta = user.DireccionCompleta,
        //        Telefono = user.Telefono,
        //        CuentaVerificada = user.CuentaVerificada,
        //        FechaIngreso = user.FechaIngreso,
        //        FechaModifico = user.FechaModifico
        //    });
        //}

       

        [HttpPost("UserRegistration")]
        public async Task<ActionResult<TblUsuariosPersonas>> PostUserRegistration(TblUsuariosPersonas user)
        {
            Random rdm = new Random();
            var code = rdm.Next(1000, 9000);

            TblUsuariosPersonas item = new TblUsuariosPersonas()
            {
                Usuario = user.Usuario,
                Nombres = user.Nombres,
                Apellidos = user.Apellidos,
                Email = user.Email,
                GeneroId = user.GeneroId,
                PaisId = 1,
                Password = Encrypter.Encrypt(user.Password),
                CodigoVerificacion = code,
                DepartamentoId = 2,
                CiudadId = 2,
                DireccionCompleta = user.DireccionCompleta,
                Telefono = user.Telefono,
                CuentaVerificada = false,
                FechaIngreso = DateTime.Now,
                FechaModifico = DateTime.Now
            };

            TblUsuarios item2 = new TblUsuarios()
            {
                Email = user.Email,
                Usuario = user.Usuario,
                Nombres = user.Nombres,
                Password = Encrypter.Encrypt(user.Password),
                IsAdmin = false
            };

            var validarUsuario = item.Usuario == "";
            if (validarUsuario)
            {
                return BadRequest("El campo usuario no puede ir vacio.");
            }

            var validarNombres = item.Nombres == "";
            if (validarNombres)
            {
                return BadRequest("El campo nombres no puede ir vacio.");
            }

            var validarApellidos = item.Apellidos == "";
            if (validarApellidos)
            {
                return BadRequest("El campo apellidos no puede ir vacio.");
            }

            var validarEmail = item.Email == "";
            if (validarEmail)
            {
                return BadRequest("El email no puede ir vacio.");
            }

            var validarTelephone = item.Telefono == null;
            if (validarTelephone)
            {
                return BadRequest("El Telefono no puede ir vacio.");
            }

            var validarPassword = item.Password == "";
            if (validarPassword)
            {
                return BadRequest("La contraseña no puede ir vacia.");
            }

            /*var validarFormatEmail = email_bien_escrito(item.Email);
            if (!validarFormatEmail)
            {
                return BadRequest("Verifique el correo, no está en el formato correcto.");
            }*/

            var emailexist = _context.TblUsuariosPersonas.FirstOrDefault(x => x.Email == item.Email);
            var usuarioexist = _context.TblUsuariosPersonas.FirstOrDefault(x => x.Usuario == item.Usuario);

            var validar = emailexist == null && usuarioexist == null;
            if (validar)
            {
                _context.TblUsuariosPersonas.Add(item);
                _context.TblUsuarios.Add(item2);
                await _context.SaveChangesAsync();

                Email obj = new Email();
                obj.SendRegistrationCode(item.Email, code);

                return Ok(new
                {
                    UsuarioPersonaId = item.UsuarioPersonaId,
                    Usuario = item.Usuario,
                    Nombres = item.Nombres,
                    Apellidos = item.Apellidos,
                    Email = item.Email,
                    GeneroId = item.GeneroId,
                    PaisId = item.PaisId,
                    Password = item.Password,
                    CodigoVerificacion = item.CodigoVerificacion,
                    DepartamentoId = item.DepartamentoId,
                    CiudadId = item.CiudadId,
                    DireccionCompleta = item.DireccionCompleta,
                    Telefono = item.Telefono,
                    CuentaVerificada = item.CuentaVerificada,
                    FechaIngreso = item.FechaIngreso,
                    FechaModifico = item.FechaModifico
                });
            }
            else
            {
                return BadRequest("El email o usuario ya esta asociado a una cuenta existente.");
            }
        }

        [HttpPost("UserVerification")]
        public async Task<ActionResult<TblUsuariosPersonas>> PostUserVerification(TblUsuariosPersonas user)
        {
            TblUsuariosPersonas item = new TblUsuariosPersonas()
            {
                UsuarioPersonaId = user.UsuarioPersonaId,
                CodigoVerificacion = user.CodigoVerificacion
            };

            var users = _context.TblUsuariosPersonas.FirstOrDefault(q => q.UsuarioPersonaId == user.UsuarioPersonaId && q.CodigoVerificacion == user.CodigoVerificacion);

            var validar = users == null;
            if (validar)
            {
                return NotFound();
            }

            _AppServices.PutUserVerification(user.UsuarioPersonaId);

            return Ok(new
            {
                UsuarioPersonaId = users.UsuarioPersonaId,
                Usuario = users.Usuario,
                Nombres = users.Nombres,
                Apellidos = users.Apellidos,
                Email = users.Email,
                GeneroId = users.GeneroId,
                PaisId = users.PaisId,
                Password = users.Password,
                CodigoVerificacion = users.CodigoVerificacion,
                DepartamentoId = users.DepartamentoId,
                CiudadId = users.CiudadId,
                DireccionCompleta = users.DireccionCompleta,
                Telefono = users.Telefono,
                CuentaVerificada = users.CuentaVerificada,
                FechaIngreso = users.FechaIngreso,
                FechaModifico = users.FechaModifico
            });
        }

 

        [HttpPut("EditUserInfo/{id}")]
        public async Task<IActionResult> PutEditUserInfo(int id, TblUsuariosPersonas user)
        {
            var data = await _context.TblUsuariosPersonas.FirstOrDefaultAsync(x => x.UsuarioPersonaId == id);

            var validar = data == null;
            if (validar)
            {
                return NotFound();
            }

            data.Usuario = user.Usuario;
            data.Nombres = user.Nombres;
            data.Apellidos = user.Apellidos;
            data.Email = user.Email;
            data.DireccionCompleta = user.DireccionCompleta;
            data.Telefono = user.Telefono;
            data.FechaModifico = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok(new
            {
                UsuarioPersonaId = data.UsuarioPersonaId,
                Usuario = data.Usuario,
                Nombres = data.Nombres,
                Apellidos = data.Apellidos,
                Email = data.Email,
                GeneroId = data.GeneroId,
                PaisId = data.PaisId,
                Password = data.Password,
                CodigoVerificacion = data.CodigoVerificacion,
                DepartamentoId = data.DepartamentoId,
                CiudadId = data.CiudadId,
                DireccionCompleta = data.DireccionCompleta,
                Telefono = data.Telefono,
                CuentaVerificada = data.CuentaVerificada,
                FechaIngreso = data.FechaIngreso,
                FechaModifico = data.FechaModifico
            });
        }

        [HttpPost("SolicitudRegistration")]
        public async Task<ActionResult<TblUsuariosAsociados>> SolicitudRegistration(TblUsuariosAsociados user)
        {

            TblUsuariosAsociados item = new TblUsuariosAsociados()
            {
                Usuario = user.Usuario,
                Nombres = user.Nombres,
                Apellidos = user.Apellidos,
                Email = user.Email,
                GeneroId = user.GeneroId,
                PaisId = 1,
                Password = Encrypter.Encrypt(user.Password),
                DepartamentoId = 2,
                CiudadId = 2,
                DireccionCompleta = user.DireccionCompleta,
                Telefono = user.Telefono,
                NombreEmpresa = user.NombreEmpresa,
                ConfirmadoPorGerencia = false
            };

            var emailexist = _context.TblUsuariosPersonas.FirstOrDefault(x => x.Email == item.Email);
            var usuarioexist = _context.TblUsuariosPersonas.FirstOrDefault(x => x.Usuario == item.Usuario);

            var validar = emailexist == null && usuarioexist == null;
            if (validar)
            {
                _context.TblUsuariosAsociados.Add(item);
                await _context.SaveChangesAsync();


                return Ok(new
                {
                    UsuarioAsociadoId = item.UsuarioAsociadoId,
                    Usuario = item.Usuario,
                    Nombres = item.Nombres,
                    Apellidos = item.Apellidos,
                    Email = item.Email,
                    GeneroId = item.GeneroId,
                    PaisId = item.PaisId,
                    Password = item.Password,
                    DepartamentoId = item.DepartamentoId,
                    CiudadId = item.CiudadId,
                    DireccionCompleta = item.DireccionCompleta,
                    Telefono = item.Telefono,
                    NombreEmpresa = item.NombreEmpresa,
                    ConfirmadoPorGerencia = item.ConfirmadoPorGerencia
                });
            }
            else
            {
                return BadRequest("El email o usuario ya esta asociado a una cuenta existente.");
            }
        }

        [HttpPut("EditAdminInfo/{id}")]
        public async Task<IActionResult> PutEditAdminInfo(int id, TblUsuariosAsociados user)
        {
            var data = await _context.TblUsuariosAsociados.FirstOrDefaultAsync(x => x.UsuarioAsociadoId == id);

            var validar = data == null;
            if (validar)
            {
                return NotFound();
            }

            data.Usuario = user.Usuario;
            data.Nombres = user.Nombres;
            data.Apellidos = user.Apellidos;
            data.Email = user.Email;
            data.DireccionCompleta = user.DireccionCompleta;
            data.Telefono = user.Telefono;

            await _context.SaveChangesAsync();
            return Ok(new
            {
                UsuarioPersonaId = data.UsuarioAsociadoId,
                Usuario = data.Usuario,
                Nombres = data.Nombres,
                Apellidos = data.Apellidos,
                Email = data.Email,
                GeneroId = data.GeneroId,
                PaisId = data.PaisId,
                Password = data.Password,
                DepartamentoId = data.DepartamentoId,
                CiudadId = data.CiudadId,
                DireccionCompleta = data.DireccionCompleta,
                Telefono = data.Telefono
            });
        }
    }
}