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

        [HttpGet("GetGenders")]
        public async Task<ActionResult> GetGenders()
        {
            var generos = await _context.TblGeneros.ToListAsync();
            return Ok(generos);
        }

        [HttpPost("UserLogin")]
        public async Task<ActionResult<TblUsuariosPersonas>> PostUserLogin(LoginDTO log)
        {
            var passencrypted = Encrypter.Encrypt(log.Password);

            var user = await _context.TblUsuariosPersonas.FirstOrDefaultAsync(x => x.Email == log.User && x.Password == passencrypted || x.Usuario == log.User && x.Password == passencrypted);

            var validar = user == null;
            if (validar)
            {
                return NotFound();
            }

            return Ok(new
            {
                UsuarioPersonaId = user.UsuarioPersonaId,
                Usuario = user.Usuario,
                Nombres = user.Nombres,
                Apellidos = user.Apellidos,
                Email = user.Email,
                GeneroId = user.GeneroId,
                PaisId = user.PaisId,
                Password = user.Password,
                CodigoVerificacion = user.CodigoVerificacion,
                DepartamentoId = user.DepartamentoId,
                CiudadId = user.CiudadId,
                DireccionCompleta = user.DireccionCompleta,
                Telefono = user.Telefono,
                CuentaVerificada = user.CuentaVerificada,
                FechaIngreso = user.FechaIngreso,
                FechaModifico = user.FechaModifico
            });
        }

        /*private Boolean email_bien_escrito(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }*/

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

            await PutUserVerification(user.UsuarioPersonaId);

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

        public async Task<IActionResult> PutUserVerification(int id)
        {
            var data = await _context.TblUsuariosPersonas.FirstOrDefaultAsync(x => x.UsuarioPersonaId == id);

            data.CuentaVerificada = true;

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}